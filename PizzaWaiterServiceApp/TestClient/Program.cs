using CustomHandlers.DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using TestClient.Models;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, world!");

            SqlConfig.SqlServer = "(localdb)\\V11.0";
            SqlConfig.SqlDatabase = "PizzaWaiter";

            Address item = new Address();
            item.UserAddress = "Pepper";
            item.UserID = 1;
            Console.WriteLine(item.UserAddress + ":");
            Response<Address> cr = item.Create();
            foreach (string message in cr.Messages)
            {
                Console.WriteLine(message);
            }
            
            item.UserAddress = "salt";
            item.UserID = 5;
            Response<Address> upd = item.Update();
            foreach (string message in upd.Messages)
            {
                Console.WriteLine(message);
            }
            Console.WriteLine(item.ID + item.UserAddress);

            Response<Address> del = item.Delete();
            foreach (string message in del.Messages)
            {
                Console.WriteLine(message);
            }
            
            Console.ReadLine();
        }
    }
}
