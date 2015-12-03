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

    public partial class Menu : System.Web.UI.Page {

        /// TODO: prop proxy
        private int RestaurantID;
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                RestaurantID = Convert.ToInt32(Request.QueryString["ID"]);
                BindMenu();
            }
        }

        protected void BindMenu() {
            this.rptMenu.DataSource = Globals.Restaurants.FirstOrDefault(x => x.ID == RestaurantID).RestaurantMenues;
            this.rptMenu.DataBind();
        }

        protected string GetMenuTitle(string id) {
            int menuId = Convert.ToInt32(id);

        }
        /*
        protected IEnumerable<Child> GetChildren(object item) {
            TestItem testitem = (TestItem)item;
            // then do whatever is necessary to get the employees from dept
            return testitem.Children;
        }
         * */
    }
}