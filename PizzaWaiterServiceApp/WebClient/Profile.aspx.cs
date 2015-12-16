using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
namespace WebClient {
    
    public partial class Profile : System.Web.UI.Page {

        private List<Address> Addresses;
        private static List<Favorite> Favorites;
        private List<ListBoxItem> listBoxItems;
        private User User;
        private int userID;
        private static bool togglePhoneForm;
        private static bool toggleAddresForm;
        

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
                Favorites = new List<Favorite>();

                this.Addresses.Add(new Address(1, 1, "Address1, user1"));
                this.Addresses.Add(new Address(2, 1, "Address2, user1"));
                this.Addresses.Add(new Address(3, 1, "Address3, user1"));
                //this.Addresses.Add(new Address(4, 2, "Address4, user2"));
                //this.Addresses.Add(new Address(5, 2, "Address5, user2"));

                Favorites.Add(new Favorite(1, 1, 9));
                Favorites.Add(new Favorite(2, 1, 8));
                //this.Favorites.Add(new Favorite(3, 2, 9));
                //this.Favorites.Add(new Favorite(4, 2, 11));

                //end todo
                toggleAddresForm = false;
                togglePhoneForm = false;

                this.BindAddresses();
                this.BindFavorites();
            }
        }
        
        private List<ListBoxItem> ToListItem()
        {
            //convert favorites to listboxitem
            listBoxItems = new List<ListBoxItem>();
            foreach (Favorite favorite in Favorites)
            {
                //we should get f.dish.name and f.dish.restaurant.name from the service and database
                listBoxItems.Add(new ListBoxItem(favorite.ID, String.Format("DishID: {0}", favorite.DishID)));
            }
            return listBoxItems;
        }

        private void BindFavorites() {
            
            this.cblFavorites.DataMember = "Key";
            this.cblFavorites.DataValueField = "Value";
            this.cblFavorites.DataSource = ToListItem();
            this.cblFavorites.DataBind();
            
        }

        private void BindAddresses() {

            this.rptAddresses.DataSource = this.Addresses;
            this.rptAddresses.DataBind();
        }

        private void ToggleEditPhone() {
            if (togglePhoneForm) {
                this.tbPhone.Visible = false;
                this.btnSavePhone.Visible = false;
                this.btnEditPhone.Visible = true;
                togglePhoneForm = false;
            } else {
                this.tbPhone.Visible = true;
                this.btnSavePhone.Visible = true;
                this.btnEditPhone.Visible = false;
                togglePhoneForm = true;
            }

        }
        private void ToggleEditAddress() {
            if (toggleAddresForm) {
                this.tbAddress.Visible = false;
                this.btnSaveAddress.Visible = false;
                this.btnCancelAddAdddress.Visible = false;
                this.btnAddAddress.Visible = true;
                toggleAddresForm = false;
            } else {
                this.tbAddress.Visible = true;
                this.btnSaveAddress.Visible = true;
                this.btnCancelAddAdddress.Visible = true;
                this.btnAddAddress.Visible = false;
                toggleAddresForm = true;
            }
        }

        protected void btnEditPhone_Click(object sender, EventArgs e) {
            this.ToggleEditPhone();
        }

        protected void btnSavePhone_Click(object sender, EventArgs e) {
            this.ToggleEditPhone();
        }

        protected void btnAddAddress_Click(object sender, EventArgs e) {
            this.ToggleEditAddress();
        }

        protected void btnCancelAddAdddress_Click(object sender, EventArgs e) {
            this.ToggleEditAddress();
        }

        protected void btnSaveAddress_Click(object sender, EventArgs e) {
            this.ToggleEditAddress();
            this.lblAddressMessage.Text = "Saved!";
        }

        protected void btnDeleteFavorites_Click(object sender, EventArgs e) {
            List<ListItem> selected = this.cblFavorites.Items.Cast<ListItem>()
            .Where(li => li.Selected).ToList();

            foreach (ListItem item in selected) {
                Favorite f = Favorites.FirstOrDefault(x => x.ID == Convert.ToInt32(item..Value));
                Favorites.Remove(f);

            }
            this.BindFavorites();
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
            this.DishID = dishId;
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