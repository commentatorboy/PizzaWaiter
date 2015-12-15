using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomHandlers.DatabaseLibrary;
using UnitTests.PizzaWaiterUnitTestServiceReference;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void UpdatePhoneForProfile()
        {
            IPizzaWaiterTestService proxy = new PizzaWaiterTestServiceClient("WSHttpBinding_IPizzaWaiterTestService");

            int userID = 1;
            bool success = proxy.UpdatePhoneNumber(userID, "333333");

            Assert.IsTrue(success);
        }

        //[TestMethod]
        public void DeleteCustomIngredientQueryString()
        {
            //SqlConnectionBuilder CB = new SqlConnectionBuilder("(localdb)\\V11.0", "PizzaWaiter", "", "");
            //Assert.IsTrue(CB.CheckConnection());

            IPizzaWaiterTestService proxy = new PizzaWaiterTestServiceClient("WSHttpBinding_IPizzaWaiterTestService");
            
            List<PartOrder> partOrders = proxy.GetPartOrdersByOrderId(5).ToList();
            
            string[] completeString = new string[partOrders.Count];
            int var = 0;
            foreach (PartOrder po in partOrders)
            {
                ///Delete from (class name) where id=(orderIdn0) OR id=(orderIdn..) OR id=(orderIdn)
                string joinPartOrdersID = string.Format("PartOrderID = {0}", po.ID);
                completeString[var] = joinPartOrdersID;
                var++;

            }
            string sqlCondition = string.Join(" OR ", completeString);

            Assert.AreEqual("PartOrderID = 5 OR PartOrderID = 6 OR PartOrderID = 7", sqlCondition);
        }
        
        //[TestMethod]
        public void ReciveOrder()
        {
            //SqlConnectionBuilder CB = new SqlConnectionBuilder("(localdb)\\V11.0", "PizzaWaiter", "", "");
            //Assert.IsTrue(CB.CheckConnection());

            IPizzaWaiterTestService proxy = new PizzaWaiterTestServiceClient("WSHttpBinding_IPizzaWaiterTestService");

            List<Order> orders = new List<Order>();
            orders = proxy.GetOrders().ToList();
            Assert.IsNotNull(orders);
        }

        [TestMethod]
        public void UpdatingDish()
        {
            SqlConnectionBuilder CB = new SqlConnectionBuilder("(localdb)\\V11.0", "PizzaWaiter", "", "");
            Assert.IsTrue(CB.CheckConnection());

            IPizzaWaiterTestService proxy = new PizzaWaiterTestServiceClient("WSHttpBinding_IPizzaWaiterTestService");


            int id = 8;
            string name = "helloworld";
            int number = 2345;
            decimal price = 234511623;
            int restaurantMenuID = 1;
            bool response = proxy.UpdateDish(id, name, price, number, restaurantMenuID);
            Assert.IsTrue(response);
        }

        //[TestMethod]
        public void PlacingTheOrder()
        {
            SqlConnectionBuilder CB = new SqlConnectionBuilder("(localdb)\\V11.0", "PizzaWaiter", "", "");
            Assert.IsTrue(CB.CheckConnection());

            IPizzaWaiterTestService proxy = new PizzaWaiterTestServiceClient("WSHttpBinding_IPizzaWaiterTestService");


            List<PartOrder> partOrders = new List<PartOrder>();

            PartOrder po1 = new PartOrder();
            po1.Amount = 2;
            po1.Dish = new Dish();
            po1.Dish.ID = 8;
            
            PartOrder po2 = new PartOrder();
            po2.Amount = 3;
            po2.Dish = new Dish();
            po2.Dish.ID = 9;

            partOrders.Add(po1);
            partOrders.Add(po2);

            //address, phonenumber
            string address = "gneooerig";
            string phoneNr = "23952754";

            

            bool success = proxy.ProcessOrder(partOrders.ToArray(), phoneNr, address);

            Assert.IsTrue(success);

        }

        /*[TestMethod]
        public void ConnectToDatabase()
        {
            SqlConnectionBuilder CB = new SqlConnectionBuilder("(localdb)\\V11.0", "PizzaWaiter", "", "");
            Assert.IsTrue(CB.CheckConnection());
        }
        */
        /*
        [TestMethod]
        public void ConnectToWCF()
        {
            IPizzaWaiterTestService proxy = new PizzaWaiterTestServiceClient();
            Restaurant r = proxy.getAllMenues(1);
            Assert.IsNull(r.Menues);
        }

        [TestMethod]
        public void ReceiveOrders()
        {
            //Prioritized test
            //Deleted test
            //Changed status test
        }
        
        
        /*[TestMethod]
        public void GetAllMenues(int RestID)
        {
            
            RestDB restdb = new RestDB();

            Rest rest = RestDB.GetByID(RestID);

            Assert.IsNull(rest);
            List<Menu> menues = Rest.Menues.OrderBy(menu => menu.Pos);
            Assert.IsNull(menues);
            foreach (Menu menu in menues)
            {
                Assert.IsNull(menu.Dishes);
                //Style all the dishes
                //print out title

                foreach (Dish dish in menu.Dishes)
                {
                    Assert.IsNull(dish.Íngredients);
                    //style them
                    //print them out
                    foreach (Ingredient i in dish.Ingredients)
                    {

                        //style them
                        //print them out
                    }
                }

            }
            
        }*/
    }
}
