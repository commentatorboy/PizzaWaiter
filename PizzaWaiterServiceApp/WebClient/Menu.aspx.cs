using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient {
    /// TODO : Replace dummie
    public class TestItem {
        public int ID { get; set; }
        public String Name { get; set; }
        public List<Child> Children { get; set; }


    }
    public class Child {

        public string Name { get; set; }
        public Child(string name) {
            this.Name = name;
        }
    }
    public partial class Menu : System.Web.UI.Page {

        /// TODO: prop proxy

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                BindMenu();
            }
        }

        protected void BindMenu() {
            /// TODO: Get menu from proxy 
            this.rptMenu.DataSource = new List<Menu>();
            this.rptMenu.DataBind();
        }

        protected IEnumerable<Child> GetChildren(object item) {
            TestItem testitem = (TestItem)item;
            // then do whatever is necessary to get the employees from dept
            return testitem.Children;
        }
    }
}