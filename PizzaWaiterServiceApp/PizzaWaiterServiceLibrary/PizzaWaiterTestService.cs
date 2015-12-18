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
            /*
            SqlConfig.SqlServer = "(localdb)\\V11.0";
            SqlConfig.SqlDatabase = "PizzaWaiter";
            */
            SqlConfig.SqlServer = "ALEXANDRALAPTOP\\SQLEXPRESS";
            SqlConfig.SqlDatabase = "PizzaWaiter";
           

        }

        public bool DeleteFavorites(List<Favorite> favorites)
        {
            FavoriteDB favoriteDB = new FavoriteDB();
            //batch delete here.
            Response<Favorite> response = favoriteDB.DeleteBatch(favorites);

            return response.Success;
        }

        public bool AddFavorite(int userID, int dishID)
        {
            Response<Favorite> response;
            FavoriteDB favoriteDB = new FavoriteDB();
            bool exists = favoriteDB.Exists(userID, dishID);

            if(!exists)
            {
                Favorite favorite = new Favorite();
                favorite.DishID = dishID;
                favorite.UserID = userID;

                response = favorite.Create();
                
            }
            else
            {
                //if the favorite does exists
                response = new Response<Favorite>();
            }
            
            return response.Success;
        }

        public List<Favorite> GetFavoritesByUserID(int userid)
        {
            //Stub
            FavoriteDB favoriteDB = new FavoriteDB();
            List<Favorite> favorites = favoriteDB.GetByUserID(userid);

            return favorites;
        }

        public User GetUserByID(int userID) {
            UserDB userDB = new UserDB();
            return userDB.GetById(userID);
        }

        public List<Address> GetAddressesByUserId(int userID) {
            AddressDB addressDB = new AddressDB();
            return addressDB.GetByUserId(userID);
        }

        public bool CreateNewUserAddress(int userID, string userAddress) {
            Address address = new Address();
            address.UserID = userID;
            address.UserAddress = userAddress;
            Response<Address> response = address.Create();
            return response.Success;
        }
        public bool DeleteAddressByID(int addressID) {
            AddressDB addressDB = new AddressDB();
            Address address  = addressDB.GetById(addressID);
            Response<Address> response = address.Delete();
            return response.Success;
        }
        public bool CreateNewDish(string dishName, int dishNumber, decimal dishPrice, int restaurantMenuID)
        {
            Dish dish = new Dish();
            dish.Name = dishName;
            dish.Price = dishPrice;
            dish.Number = dishNumber;
            dish.RestaurantMenuID = restaurantMenuID;
            Response<Dish> response = dish.Create();
            
            return response.Success;
        }

        public bool UpdateDish(int dishID, string dishName, decimal dishPrice, int dishNumber, int dishRestaurantMenuID)
        {
            DishDB dishDB = new DishDB();
            Dish dish = dishDB.GetById(dishID);
            dish.Name = dishName;
            dish.Price = dishPrice;
            dish.Number = dishNumber;
            dish.RestaurantMenuID = dishRestaurantMenuID;
            Response<Dish> response = dish.Update();
            foreach (string r in response.Messages)
            {
                Console.WriteLine(r);
            }

            return response.Success;
        }



        public bool DeleteDishByID(int dishID)
        {
            DishDB dishDB = new DishDB();
            Dish dish = dishDB.GetById(dishID);
            Response<Dish> response = dish.Delete();
            return response.Success;

        }

        public List<Dish> GetDishesByRestaurantID(int restaurantID)
        {
            DishDB dishDB = new DishDB();
            List<Dish> dishes = dishDB.GetDishesByRestaurantID(restaurantID);
            return dishes;
        }

        public void ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {
            OrderDB orderDB = new OrderDB();
            Order order = orderDB.GetById(orderId);
            order.StatusID = newStatus;
            order.Update();
        }

        /// <summary>
        ///  TODO: change to boolean
        /// </summary>
        /// <param name="orderID"></param>
        public void DeleteOrderByID(int orderID)
        {
            OrderDB orderDB = new OrderDB();
            Order order = orderDB.GetById(orderID);
            //Make a check for nulls
            
            order.Delete();
        }

        public List<PartOrder> GetPartOrdersByOrderId(int orderID)
        {
            PartOrderDB poDB = new PartOrderDB();
            return poDB.GetByOrderId(orderID);
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
            foreach (string s in r.Messages)
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
        public List<RestaurantMenu> GetRestaurantMenues(int restaurantID)
        {

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

        public bool UpdatePhoneNumber(int userID, string phoneNumber)
        {
            /*
            UserDB userDB = new UserDB();
            User user = userDB.GetById(userID);
            user.PhoneNumber = phoneNumber;
            Response<User> response = user.Update();
            return response.Success;*/

            UserDB userDB = new UserDB();
            User user = userDB.GetById(userID);
            user.PhoneNumber = phoneNumber;
            Response<User> response = user.Update();
            if (!response.Success) {
                User updatedUser = userDB.GetById(userID);
                if (updatedUser.PhoneNumber == phoneNumber) {
                    response.Success = true;
                }
            }
            return response.Success;
        }

    }

}
