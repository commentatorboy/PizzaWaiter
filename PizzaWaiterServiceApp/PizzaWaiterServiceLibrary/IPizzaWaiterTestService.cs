using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Models;

namespace PizzaWaiterServiceLibrary {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPizzaWaiterTestService {

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
