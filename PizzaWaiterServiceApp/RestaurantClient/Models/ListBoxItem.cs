using RestaurantClient.PizzaWaiterTestServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    class ListBoxItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ListBoxItem(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
