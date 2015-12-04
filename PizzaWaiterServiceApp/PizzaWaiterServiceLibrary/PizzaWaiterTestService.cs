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
