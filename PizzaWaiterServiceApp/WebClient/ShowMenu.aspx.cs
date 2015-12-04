using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.PizzaWaiterTestServiceReference;
using WebClient.Models;

namespace WebClient {
    /// TODO : Replace dummie

    public partial class ShowMenu : System.Web.UI.Page {

        /// TODO: prop proxy
        IPizzaWaiterTestService proxy;
        private int RestaurantID;
        List<RestaurantMenu> restaurantMenues; 
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                proxy = Proxy.Get();
                RestaurantID = Convert.ToInt32(Request.QueryString["ID"]);
                if (RestaurantID>0) {
                    restaurantMenues = proxy.GetRestaurantMenues(RestaurantID).ToList();
                    BindMenu();
                }
                
            }
        }

        protected void BindMenu() {

            this.rptMenu.DataSource = this.restaurantMenues.OrderBy(x=>x.Position);
            this.rptMenu.DataBind();
        }

        

        protected string FormatMenuTitle(WebClient.PizzaWaiterTestServiceReference.Menu m) {
            return m.Title;
        }
        
        protected IEnumerable<Dish> GetDishes(object item) {
            RestaurantMenu rm = (RestaurantMenu)item;
            List<Dish> dl = proxy.GetDishesByRestaurantMenuId(rm.ID).ToList();
            return dl;
        }

        protected string FormatIngredientName(WebClient.PizzaWaiterTestServiceReference.Ingredient i)
        {
            string result = "";
            if (i!=null) {
                result = i.Name;
            }
            return i.Name;
        }

        protected IEnumerable<DishIngredient> GetIngredients(object item)
        {
            Dish di = (Dish)item;

            List<DishIngredient> il = proxy.GetIngredientsByDishId(di.ID).ToList();
            return il;
        }

        protected void blAddToOrder(object sender, CommandEventArgs e) {
            
            int dishId = Convert.ToInt32(e.CommandArgument);
            this.ltTest.Text = "adding dish...." + dishId;
        }


    }
}