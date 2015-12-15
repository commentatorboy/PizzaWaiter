using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Models;
using CustomHandlers.DatabaseLibrary;

namespace PizzaWaiterServiceLibrary {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPizzaWaiterTestService
    {

        [OperationContract]
        bool CreateNewDish(string dishName, int dishNumber, decimal dishPrice, int restaurantID);
        [OperationContract]
        bool UpdateDish(int dishID, string dishName, decimal dishPrice, int dishNumber, int dishRestaurantMenuID);
        [OperationContract]
        bool DeleteDishByID(int dishID);
        [OperationContract]
        List<Dish> GetDishesByRestaurantID(int restaurantID);
        [OperationContract]
        void ChangeOrderStatus(int OrderId, OrderStatus newStatus);
        [OperationContract]
        void DeleteOrderByID(int orderID);
        [OperationContract]
        List<PartOrder> GetPartOrdersByOrderId(int orderID);
        [OperationContract]
        List<Order> GetOrders();
        [OperationContract]
        Dish GetDishById(int dishID);
        [OperationContract]
        bool ProcessOrder(List<PartOrder> partOrders, string phoneNr, string address);
        [OperationContract]
        List<DishIngredient> GetIngredientsByDishId(int dishID);
        [OperationContract]
        List<Dish> GetDishesByRestaurantMenuId(int restaurantMenuID);
        [OperationContract]
        List<RestaurantMenu> GetRestaurantMenues(int restaurantID);
        [OperationContract]
        List<Restaurant> GetLocalRestaurants(decimal latitude, decimal longtitude);
    }


}
