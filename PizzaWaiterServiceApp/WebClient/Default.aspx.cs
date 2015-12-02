using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.PizzaWaiterTestServiceReference;




namespace WebClient {
    public class TestItem
    {
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


    public partial class Default : System.Web.UI.Page {

        //public static IElectricCarsUserService proxy = new ElectricCarsUserServiceClient();
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                this.GetRestaurants();
            }
        }
        protected void GetRestaurants() {
            List<TestItem> restaurants = new List<TestItem>();
            TestItem r1 = new TestItem();
            r1.ID = 1;
            r1.Name = "Red Sails";
            r1.Children = new List<Child>();
            r1.Children.Add(new Child("Child 1"));
            r1.Children.Add(new Child("Child 2"));
            TestItem r2 = new TestItem();
            r2.ID = 2;
            r2.Name = "Coffee shop";
            r2.Children = new List<Child>();
            r2.Children.Add(new Child("Child 3"));
            r2.Children.Add(new Child("Child 4"));
            
            restaurants.Add(r1);
            restaurants.Add(r2);
            rptRestaurants.DataSource = restaurants;
            rptRestaurants.DataBind();

        }

        protected IEnumerable<Child> GetChildren(object item) {
            TestItem testitem= (TestItem)item;
            // then do whatever is necessary to get the employees from dept
            return testitem.Children;
        }
    }
}