﻿using System;
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

        private SortOrder direction;

        public RestaurantClientForm()
        {
            InitializeComponent();
            orders = Program.proxy.GetOrders().ToList();
            direction = SortOrder.Ascending;
            this.BindOrders();
            
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
            if (this.dgvShowOrders.SelectedRows.Count == 1)
            {
                DataGridViewRow row = this.dgvShowOrders.SelectedRows[0];
                int orderID = Convert.ToInt32(row.Cells["ID"].Value);
                List<PartOrder> partOrders = Program.proxy.GetPartOrdersByOrderId(orderID).ToList();
                List<ListBoxItem> dataSource = this.ToListBoxItem(partOrders);
                ((ListBox)clbPartOrdersList).DataSource = dataSource;
                ((ListBox)clbPartOrdersList).DisplayMember = "Name";
                ((ListBox)clbPartOrdersList).ValueMember = "ID";
                Order order = orders.FirstOrDefault(x => x.ID == orderID);
                this.lblAddress.Text = string.Format("Address: {0}",order.Address.UserAddress);
                this.lblPhoneNr.Text = string.Format("PhoneNumber: {0}",order.User.PhoneNumber);
                this.lblTotalPrice.Text = string.Format("Total: {0:0.00}kr",this.CalculateTotalPrice(partOrders));
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

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {

            DataGridView grid = (DataGridView)sender;
            DataGridViewRow row = grid.Rows[e.RowIndex];
            DataGridViewColumn col = grid.Columns[e.ColumnIndex];
            if (row.DataBoundItem != null && col.DataPropertyName.Contains(".")) {
                string[] props = col.DataPropertyName.Split('.');
                PropertyInfo propInfo = row.DataBoundItem.GetType().GetProperty(props[0]);
                object val = propInfo.GetValue(row.DataBoundItem, null);
                for (int i = 1; i < props.Length; i++) {
                    propInfo = val.GetType().GetProperty(props[i]);
                    val = propInfo.GetValue(val, null);
                }
                e.Value = val;
            }
        }

        private void BindOrders() {
            orders = direction == SortOrder.Ascending ? orders.OrderBy(x => x.Created).ToList() : orders.OrderByDescending(x => x.Created).ToList();
            this.dgvShowOrders.DataSource = orders;
        }

        private void dgvShowOrders_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            this.direction = direction == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            this.BindOrders();
        }
    }
}
