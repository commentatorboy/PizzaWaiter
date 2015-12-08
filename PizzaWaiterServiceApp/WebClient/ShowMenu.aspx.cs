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
        static List<PartOrder> order;

        protected void Page_Load(object sender, EventArgs e) {
            proxy = Proxy.Get();
            if (!IsPostBack) {
                
                RestaurantID = Convert.ToInt32(Request.QueryString["ID"]);
                if (RestaurantID>0) {
                    restaurantMenues = proxy.GetRestaurantMenues(RestaurantID).ToList();
                    BindMenu();
                }
                order = new List<PartOrder>(); 
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
            //order = (List<PartOrder>)Session["order"];
            int dishId = Convert.ToInt32(e.CommandArgument);
            this.ltTest.Text = "adding dish...." + dishId;

            List<PartOrder> sameDishes = order.Where(x => x.DishID == dishId).ToList();
            
            if (sameDishes.Count==0) {
                //add part order
                this.AddPartOrder(dishId);
            } else {
                PartOrder unchanged = sameDishes.FirstOrDefault(x => x.CustomIngredients.Count() == 0);
                if (unchanged!=null) {
                    unchanged.Amount++;
                } else {
                    this.AddPartOrder(dishId);
                }
                // found unchanged dish in order, and new dish is unchanged => increment
                // new dish is changed and a changes match is found in order => increment
                // new dish is changed and a changes match is not found in order => new part order

                
            }
            this.rptOrder.DataSource = order;
            this.rptOrder.DataBind();
        }

        protected void AddPartOrder(int dishId) {
            PartOrder po = new PartOrder();
            po.Dish = proxy.GetDishById(dishId);
            po.Amount = 1;
            order.Add(po);
        }

        protected void IncrementPartOrder() {
        }

        protected string FormatPartOrder(object item) {

            // po has only id. 
            //in order to print info need to get full object from server
            PartOrder po =  (PartOrder)item;
            string result = string.Format("{0} - {1}x{2} kr",po.Dish.Name,po.Amount,po.Dish.Price);
            return result;
        }
    }
}