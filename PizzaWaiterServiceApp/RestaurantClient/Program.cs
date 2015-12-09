using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantClient.PizzaWaiterTestServiceReference;

namespace RestaurantClient
{
    static class Program
    {
        public static IPizzaWaiterTestService proxy = new PizzaWaiterTestServiceClient();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RestaurantClientForm(proxy));
        }
    }
}
