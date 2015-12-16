using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient {
    
    public partial class Profile : System.Web.UI.Page {

        private List<Address> Addresses;
        private List<Favorite> Favorites;
        private User User;
        private int userID;
        

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {

                //TODO: replace with session
                // this.UserID = Session["UserID"];
                // TODO: if no session or user doesnt exist, 
                // then redirect to login
          
                // TODO: this.User = proxy.GetUserByID(this.UserID);
                this.User = new User(1, "+45 7894563210");
                
                // TODO: Get data about addresses and favorites from server
                // this.Addresses = proxy.GetAddressesByUserID(this.UserID);
                // this.Favorites = proxy.GetFavoritesByUserID(this.UserID);
                this.Addresses = new List<Address>();
                this.Favorites = new List<Favorite>();

                this.Addresses.Add(new Address(1, 1, "Address1, user1"));
                this.Addresses.Add(new Address(2, 1, "Address2, user1"));
                this.Addresses.Add(new Address(3, 1, "Address3, user1"));
                //this.Addresses.Add(new Address(4, 2, "Address4, user2"));
                //this.Addresses.Add(new Address(5, 2, "Address5, user2"));

                this.Favorites.Add(new Favorite(1, 1, 1));
                this.Favorites.Add(new Favorite(2, 1, 8));
                //this.Favorites.Add(new Favorite(3, 2, 9));
                //this.Favorites.Add(new Favorite(4, 2, 11));

                //end todo
                this.BindAddresses();
                this.BindFavorites();
            }
        }

        private void BindFavorites() {
            
        }

        private void BindAddresses() {
            this.rptAddresses.DataSource = this.Addresses;
            this.rptAddresses.DataBind();
        }

        private void ToggleEditPhone() {
        }
        private void ToggleEditAddress() {
        }
    }

    









    public class Favorite
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int DishID { get; set; }

        public Favorite(int id, int userId, int dishId) {
            this.ID = id;
            this.UserID = userId;
            this.DishID = DishID;
        }
    }
    public class Address
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserAddress { get; set; }

        public Address(int id, int userID, string userAddress) {
            this.ID = id;
            this.UserID = UserID;
            this.UserAddress = userAddress;
        }
    }

    public class User {
        public int ID { get; set; }
        public string PhoneNumber { get; set; }

        public User(int userID, string phoneNumber) {
            this.ID = userID;
            this.PhoneNumber = phoneNumber;
        }
    }
}