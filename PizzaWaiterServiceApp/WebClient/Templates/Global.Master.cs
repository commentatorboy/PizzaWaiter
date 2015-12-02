using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.PizzaWaiterTestServiceReference;

namespace WebClient.Templates {
    public partial class Global : System.Web.UI.MasterPage {
        public static IPizzaWaiterTestService proxy = new PizzaWaiterTestServiceClient();
        protected void Page_Load(object sender, EventArgs e) {

        }
    }
}