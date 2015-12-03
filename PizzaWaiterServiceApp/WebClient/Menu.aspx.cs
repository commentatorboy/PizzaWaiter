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
            /// TODO: Get menu from proxy 
            IPizzaWaiterTestService p = Proxy.Get();
            Restaurant r = p.getAllMenues(RestaurantID);
            this.ltRestaurantTitle.Text = r.Name;
            
            this.rptMenu.DataSource = r.Menues;
            this.rptMenu.DataBind();
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