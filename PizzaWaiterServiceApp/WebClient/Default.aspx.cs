using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.Models;
using WebClient.PizzaWaiterTestServiceReference;




namespace WebClient {
    public partial class Default : System.Web.UI.Page {
        
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                this.GetRestaurants();
            }
        }
        protected void GetRestaurants() {
            /// TODO: Create method at service
            IPizzaWaiterTestService p = Proxy.Get();
            Globals.Restaurants = p.GetLocalRestaurants(1, 1).ToList();
            /*
            Restaurant r = new Restaurant();
            r.ID = 1;
            r.Name = "Red Sails";
            */
            //restaurants.Add();
            rptRestaurants.DataSource = Globals.Restaurants;
            rptRestaurants.DataBind();
            
             

        }
    }
}