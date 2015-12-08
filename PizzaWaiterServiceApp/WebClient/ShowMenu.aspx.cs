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

            List<PartOrder> sameDishes = order.Where(x => x.Dish.ID == dishId).ToList();
            
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
            this.BindOrder();
        }

        protected string GetDishIdFromPartOrder(object item)
        {
            string result = "";
            PartOrder po = (PartOrder) item;
            result = po.Dish.ID.ToString();
            return result;
        }
        protected string GetDishIngredientsFromPartOrder(object item)
        {
            string result = "";
            PartOrder po = (PartOrder) item;
            return result;
        }
        protected void removeDishFromOrder(object sender, CommandEventArgs e) {
            int dishID = Convert.ToInt32(e.CommandArgument);
            List<PartOrder> match = order.Where(x => x.Dish.ID == dishID).ToList();

            string ingredients = e.CommandName;
            if (ingredients == "") {
                PartOrder po = match.FirstOrDefault(x => x.CustomIngredients.Count() == 0);
                if (po.Amount==1) {
                    order.Remove(po);
                } else {
                    po.Amount--;
                }
            }
            this.BindOrder();

        }
        protected void BindOrder() {
            this.rptOrder.DataSource = order;
            this.rptOrder.DataBind();
        }
        protected void AddPartOrder(int dishId) {
            PartOrder po = new PartOrder();
            po.Dish = proxy.GetDishById(dishId);
            /// TODO: convertion to array can give trouble, solve it if occures
            List<CustomIngredient> cis = new List<CustomIngredient>();
            po.CustomIngredients = cis.ToArray();
            po.Amount = 1;
            order.Add(po);
        }

        protected void IncrementPartOrder() {
        }

        protected string CalculatePrice() {
            decimal priceTotal = 0;
            if (order.Count>0) {
                foreach (PartOrder po in order) {
                    int amount = po.Amount;
                    decimal dishPrice = po.Dish.Price;
                    priceTotal += amount * dishPrice;
                }
            }
            return String.Format("{0:0.00}", priceTotal);
        }
        protected string FormatPartOrder(object item) {

            // po has only id. 
            //in order to print info need to get full object from server
            PartOrder po =  (PartOrder)item;
            string result = string.Format("{0} - {1}x{2} kr",po.Dish.Name,po.Amount,po.Dish.Price);
            return result;
        }

        protected void btnSubmitOrder_Click(object sender, EventArgs e) {
            string phoneNr = this.txtPhoneNr.Text;
            string address = this.txtAddress.Text;

            if (phoneNr!="" && address !="" && order.Count()!=0) {
                if(proxy.ProcessOrder(order.ToArray(), phoneNr, address))
                {
                    this.ltConfirmation.Text = "Order is sent";
                }
            } else {
                this.ltConfirmation.Text = "Fill up the hone number, address and the order";
            }
        }
    }
}