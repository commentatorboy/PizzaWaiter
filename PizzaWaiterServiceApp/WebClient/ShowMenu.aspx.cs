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
        /*
        protected IEnumerable<Dish> GetDishes(object item) {
            RestaurantMenu rm = (RestaurantMenu)item;
            List<Dish> dl = proxy.GetDishesByRestaurantMenuId();
            return dl;
        }
         */
    }
}