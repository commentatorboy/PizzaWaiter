using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomHandlers.DatabaseLibrary;


namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void ConnectToDatabase()
        {
            SqlConnectionBuilder CB = new SqlConnectionBuilder("(localdb)\\V11.0", "PizzaWaiter", "", "");
            Assert.IsTrue(CB.CheckConnection());
        }

        /*
        
        [TestMethod]
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
