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
        private int dishID;
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
            this.BindStatus();
            
        }

        #region Dishes
        private void BindDishes()
        {

            dishes = direction == SortOrder.Ascending ? dishes.OrderBy(x => x.RestaurantMenu.Position).ToList() : dishes.OrderByDescending(x => x.RestaurantMenu.Position).ToList();

            ///TODO: make it sort by menu
            //this.dgvShowDishes.ClearSelection();
            //this.dgvShowDishes.Refresh();
            if (dishes.Count > 0)
            {
                this.dgvShowDishes.DataSource = dishes;
            }
            else
            {
                this.dgvShowDishes.DataSource = null;
            }
            
            
            this.UpdateDishInfo();

        }
        private Dish GetSelectedDish() {
            Dish dish = null;
            if (this.dgvShowDishes.SelectedRows.Count > 0) {
                DataGridViewRow row = this.dgvShowDishes.SelectedRows[0];
                this.dishID = Convert.ToInt32(row.Cells["iDDataGridViewTextBoxColumn"].Value);
                dish = dishes.FirstOrDefault(x => x.ID == this.dishID);
            }
            return dish;
        }
        private void dgvShowDishes_SelectionChanged(object sender, EventArgs e) {
            this.btnDeleteDish.Enabled = true;
            if (this.dgvShowDishes.SelectedRows.Count == 1) {
                this.UpdateDishInfo();
            }
        }

        private void UpdateDishInfo() {
            Dish dish = this.GetSelectedDish();
            if (dish!= null) {
                this.tbDishName.Text = dish.Name;
                this.tbDishNumber.Text = dish.Number.ToString();
                this.tbDishPrice.Text = String.Format("{0:0.00}",dish.Price);
            }
        }
        private void btnDeleteDish_Click(object sender, EventArgs e) {
            /// STUB: 
            if (Program.proxy.DeleteDishByID(this.dishID)) {
                this.dishes.Remove(this.GetSelectedDish());
                this.BindDishes();

            }

            
            
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
        private void BindStatus()
        {

            Order selectedOrder = this.GetSelectedOrder();
            OrderStatus status;
            if (selectedOrder == null)
            {
                status = OrderStatus.Default;
            }
            else
            {
                status = selectedOrder.StatusID;
            }

            this.cbStatus.Items.Clear();
            this.cbStatus.DisplayMember = "Name";
            this.cbStatus.ValueMember = "ID";
            foreach (int item in Enum.GetValues(typeof(OrderStatus)))
            {
                this.cbStatus.Items.Add(new ListBoxItem(item, Enum.GetName(typeof(OrderStatus), item)));
                if ((int)status == item)
                {
                    this.cbStatus.SelectedItem = this.cbStatus.Items[item];
                }
            }
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
            this.btnDeleteOrder.Enabled = true;

            if (this.dgvShowOrders.SelectedRows.Count == 1)
            {
                //DataGridViewRow row = this.dgvShowOrders.SelectedRows[0];
                //orderID = this.GetSelectedOrder().ID;
                this.UpdateOrderInfo();
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
            this.UpdateOrderInfo();
            
        }

        private void dgvShowOrders_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            this.direction = direction == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            this.BindOrders();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Program.proxy.DeleteOrderByID(orderID);
            Order deleteOrder = this.GetSelectedOrder();
            orders.Remove(deleteOrder);
            this.BindOrders();
        }

        private void UpdateOrderInfo()
        {
            Order order = this.GetSelectedOrder();

            ((ListBox)clbPartOrdersList).DisplayMember = "Name";
            ((ListBox)clbPartOrdersList).ValueMember = "ID";
            if (order != null)
            {
                
                List<PartOrder> partOrders = Program.proxy.GetPartOrdersByOrderId(orderID).ToList();
                List<ListBoxItem> dataSource = this.ToListBoxItem(partOrders);
                ((ListBox)clbPartOrdersList).DataSource = dataSource;
                this.lblAddress.Text = string.Format("Address: {0}", order.Address.UserAddress);
                this.lblPhoneNr.Text = string.Format("PhoneNumber: {0}", order.User.PhoneNumber);
                this.lblTotalPrice.Text = string.Format("Total: {0:0.00}kr", this.CalculateTotalPrice(partOrders));

            }
            else
            {
                ((ListBox)clbPartOrdersList).DataSource = new List<ListBoxItem>();
                this.lblAddress.Text = "";
                this.lblPhoneNr.Text = "";
                this.lblTotalPrice.Text = "";
                
            }
            this.BindStatus();
        }

        private Order GetSelectedOrder() {
            Order order = null;
            if (this.dgvShowOrders.SelectedRows.Count>0) {
                DataGridViewRow row = this.dgvShowOrders.SelectedRows[0];
                this.orderID = Convert.ToInt32(row.Cells["ID"].Value);
                order =  orders.FirstOrDefault(x => x.ID == this.orderID);    
            }
                /*
            else
	        {
                order = new Order();
	        }*/
            return order;
            }

        private void btnChangeStatus_Click(object sender, EventArgs e) {
            ListBoxItem item = (ListBoxItem)this.cbStatus.SelectedItem;
            OrderStatus newStatus = (OrderStatus)item.ID;
            Order order = this.GetSelectedOrder();
            //update in database
            Program.proxy.ChangeOrderStatus(order.ID, newStatus);
            //update locally
            order.StatusID = newStatus;
            //update interface
            this.BindOrders();
        }
        #endregion





    }
}
