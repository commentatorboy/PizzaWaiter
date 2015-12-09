using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Models;
using CustomHandlers.DatabaseLibrary;

namespace PizzaWaiterServiceLibrary {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class PizzaWaiterTestService : IPizzaWaiterTestService {


        public PizzaWaiterTestService()
        {
            
            SqlConfig.SqlServer = "(localdb)\\V11.0";
            SqlConfig.SqlDatabase = "PizzaWaiter";
            /*
            SqlConfig.SqlServer = "ALEXANDRALAPTOP\\SQLEXPRESS";
            SqlConfig.SqlDatabase = "PizzaWaiter";
            */
        }

        public List<Order> GetOrders()
        {
            OrderDB orderDB = new OrderDB();
            return orderDB.GetAll();
        }

        public Dish GetDishById(int dishID)
        {
            DishDB dishDB = new DishDB();
            
            return dishDB.GetById(dishID);
        }

        public bool ProcessOrder(List<PartOrder> partOrders, string phoneNr, string address)
        {
            
            //Create the user to be inserted into the database.
            UserDB userDB = new UserDB();
            User user = userDB.GetUserByPhone(phoneNr);
            
            //if the user does not exist already, create one.
            if (user == null)
            {
                user = new User();
                user.PhoneNumber = phoneNr;
                user.RankID = 0;
                user.Create();
            }


            //Create the address to be inserted into the database.
            AddressDB addressDB = new AddressDB();
            Address userAddress = addressDB.GetByAddress(address);

            //if the address does not exist already, create one.
            if (userAddress == null)
            {
                userAddress = new Address();
                userAddress.UserAddress = address;
                userAddress.UserID = user.ID;
                userAddress.Create();
                
            }
            
            //Create the order to be inserted to the database
            Order order = new Order();
            order.AddressID = userAddress.ID;
            order.UserID = user.ID;
            order.StatusID = OrderStatus.WAITING;
            order.Create();

            //The partorders are inserted to the database
            PartOrderDB poDB = new PartOrderDB();

            //For debugging Part Orders when something happens.
            CustomHandlers.DatabaseLibrary.Response<PartOrder> r = poDB.CreateBatch(order.ID, partOrders);
            foreach(string s in r.Messages)
            {
                Console.WriteLine(s);
            }

            return r.Success;
        }

        public List<DishIngredient> GetIngredientsByDishId(int dishID)
        {
            DishIngredientDB dishIngredientDB = new DishIngredientDB();
            return dishIngredientDB.GetByDishId(dishID);
        }

        public List<Dish> GetDishesByRestaurantMenuId(int restaurantMenuID)
        {
            DishDB dishDB = new DishDB();
            return dishDB.GetByRestaurantMenuId(restaurantMenuID);
        }


        public List<Restaurant> GetLocalRestaurants(decimal latitude, decimal longtitude)
        {
            //List<Restaurant> restaurants = new List<Restaurant>();
            RestaurantDB rDB = new RestaurantDB();

            /// TODO: Use Lat and Long in the future.
            List<Restaurant> rl = rDB.GetAll();
            return rl;
            //return restaurants;
            /*
            Restaurant r = new Restaurant();
            r.ID = 1;
            r.Name = "Red Sails";
            r.Menues = new List<Menu>();

            Menu m1 = new Menu();
            m1.ID = 1;
            m1.Title = "Pizza";

            Menu m2 = new Menu();
            m2.ID = 2;
            m2.Title = "Drinks";

            r.Menues.Add(m1);
            r.Menues.Add(m2);

            restaurants.Add(r);
            return restaurants;
            */
        }
        public List<RestaurantMenu> GetRestaurantMenues(int restaurantID) {
            
            RestaurantMenuDB restaurantMenuDB = new RestaurantMenuDB();
            List<RestaurantMenu> rm = restaurantMenuDB.GetByRestaurantId(restaurantID);
            
/*
            Restaurant r = new Restaurant();
            r.ID = 1;
            r.Name = "Red Sails";
            r.Menues = new List<Menu>();

            Menu m1 = new Menu();
            m1.ID = 1;
            m1.Title = "Pizza";

            Menu m2 = new Menu();
            m2.ID = 2;
            m2.Title = "Drinks";

            r.Menues.Add(m1);
            r.Menues.Add(m2);
            return r;*/

            return rm;
        }
    }
}
