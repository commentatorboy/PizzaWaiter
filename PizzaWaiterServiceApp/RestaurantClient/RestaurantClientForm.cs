using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantClient.PizzaWaiterTestServiceReference;

namespace RestaurantClient
{
    public partial class RestaurantClientForm : Form
    {
        private List<Order> orders;
        private int orderID;
        private SortOrder direction;
        private List<Dish> dishes;


        public RestaurantClientForm()
        {
            InitializeComponent();
            orderID = 0;
            orders = Program.proxy.GetOrders().ToList();
            ///TODO: This is the part the Restaurant Login
            dishes = Program.proxy.GetDishesByRestaurantID(2).ToList();

            direction = SortOrder.Ascending;
            this.BindOrders();
            this.BindDishes();
            this.BindStatus(OrderStatus.Default);
            
        }

        #region Dishes
        private void BindDishes()
        {
            ///TODO: make it sort by menu
            this.dgvShowDishes.DataSource = dishes;

        }

        #endregion

        #region common
        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            DataGridView grid = (DataGridView)sender;
            DataGridViewRow row = grid.Rows[e.RowIndex];
            DataGridViewColumn col = grid.Columns[e.ColumnIndex];
            if (row.DataBoundItem != null && col.DataPropertyName.Contains("."))
            {
                string[] props = col.DataPropertyName.Split('.');
                PropertyInfo propInfo = row.DataBoundItem.GetType().GetProperty(props[0]);
                object val = propInfo.GetValue(row.DataBoundItem, null);
                for (int i = 1; i < props.Length; i++)
                {
                    propInfo = val.GetType().GetProperty(props[i]);
                    val = propInfo.GetValue(val, null);
                }
                e.Value = val;
            }
        }
        #endregion


        #region Orders
        private void BindStatus(OrderStatus status)
        {
            this.cbStatus.Items.Clear();
            this.cbStatus.DisplayMember = "Name";
            this.cbStatus.ValueMember = "ID";
            foreach (int item in Enum.GetValues(typeof(OrderStatus))) {
                this.cbStatus.Items.Add(new ListBoxItem(item, Enum.GetName(typeof(OrderStatus),item)));
            }
            this.cbStatus.SelectedValue = OrderStatus.Default;
        }

        private List<ListBoxItem> ToListBoxItem(List<PartOrder> partOrders)
        {
            List<ListBoxItem> list = new List<ListBoxItem>();
            foreach (PartOrder po in partOrders)
            {
                list.Add(new ListBoxItem(po.ID, String.Format("{0} - {1}x{2}kr", po.Dish.Name, po.Amount, po.Dish.Price)));
            }
            return list;
        }

        private void dgvShowOrders_SelectionChanged(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = true;

            if (this.dgvShowOrders.SelectedRows.Count == 1)
            {
                DataGridViewRow row = this.dgvShowOrders.SelectedRows[0];
                orderID = Convert.ToInt32(row.Cells["ID"].Value);
                this.updateInfo(orderID);
            }
        }

        private decimal CalculateTotalPrice(List<PartOrder> partOrders)
        {
            decimal totalPrice = 0;
            foreach(PartOrder po in partOrders)
            {
                totalPrice += po.Dish.Price * po.Amount;
            }
            return totalPrice;
        }


        private void BindOrders() {
            orders = direction == SortOrder.Ascending ? orders.OrderBy(x => x.Created).ToList() : orders.OrderByDescending(x => x.Created).ToList();
            this.dgvShowOrders.DataSource = orders;
            
        }

        private void dgvShowOrders_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            this.direction = direction == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            this.BindOrders();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Program.proxy.DeleteOrderByID(orderID);
            Order deleteOrder = orders.FirstOrDefault(x => x.ID == orderID);
            orders.Remove(deleteOrder);
            this.BindOrders();
            this.updateInfo(0);
        }

        private void updateInfo(int orderID)
        {
            ((ListBox)clbPartOrdersList).DisplayMember = "Name";
            ((ListBox)clbPartOrdersList).ValueMember = "ID";
            if (orderID > 0)
            {
                
                List<PartOrder> partOrders = Program.proxy.GetPartOrdersByOrderId(orderID).ToList();
                List<ListBoxItem> dataSource = this.ToListBoxItem(partOrders);
                ((ListBox)clbPartOrdersList).DataSource = dataSource;
                
                Order order = orders.FirstOrDefault(x => x.ID == orderID);
                this.lblAddress.Text = string.Format("Address: {0}", order.Address.UserAddress);
                this.lblPhoneNr.Text = string.Format("PhoneNumber: {0}", order.User.PhoneNumber);
                this.lblTotalPrice.Text = string.Format("Total: {0:0.00}kr", this.CalculateTotalPrice(partOrders));
                this.BindStatus(order.StatusID);
            }
            else
            {
                ((ListBox)clbPartOrdersList).DataSource = new List<ListBoxItem>();
                this.lblAddress.Text = "";
                this.lblPhoneNr.Text = "";
                this.lblTotalPrice.Text = "";
                this.BindStatus(OrderStatus.Default);
            }
        }
        #endregion
    }
}
