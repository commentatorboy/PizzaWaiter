using CustomHandlers.DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, world!");

            SqlConfig.SqlServer = "(localdb)\\V11.0";
            SqlConfig.SqlDatabase = "PizzaWaiter";

            Menu item = new Menu();
            item.Title = "item1";
            Response<Menu> cr = item.Create();
            foreach (string message in cr.Messages)
            {
                Console.WriteLine(message);
            }

            item.Title = "Menu 1 Updated";
            Response<Menu> upd = item.Update();
            foreach (string message in upd.Messages)
            {
                Console.WriteLine(message);
            }
            Console.WriteLine(item.ID + ":" + item.Title);

            Response<Menu> del = item.Delete();
            foreach (string message in del.Messages)
            {
                Console.WriteLine(message);
            }

            Console.ReadLine();
        }
    }
}
