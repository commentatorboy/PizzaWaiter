using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomHandlers.DatabaseLibrary;

namespace Models {

    /* Describes an object */
    public class RestaurantMenu : SqlModel {

        private RestaurantMenuDB restaurantMenuDB; //related DB handler (required)

        private RestaurantDB restaurantDB;
        private MenuDB menuDB;

        /*Object properties (custom)*/
        public int ID { get; set; }
        public int MenuID { get; set; }
        public int RestaurantID { get; set; }
        public int Position { get; set; }

        public Menu Menu
        {
            get
            {
                return this.GetMenu();
            }
            set
            {
            }
        }



        public Restaurant Restaurant { get; set; }
        



        /*Build Object (required)*/
        public void BuildObject(DataRow row) {
            this.ID = SqlFormat.ToInt(row, "ID");
            this.MenuID = SqlFormat.ToInt(row, "MenuID");
            this.RestaurantID = SqlFormat.ToInt(row, "RestaurantID");
            this.Position = SqlFormat.ToInt(row, "Position");

            //this.Connect();
            //this.Menu = menuDB.GetById(SqlFormat.ToInt(row, "MenuID"));
            //this.Restaurant = restaurantDB.GetById(SqlFormat.ToInt(row, "RestaurantID"));
            
        }


        private Menu GetMenu()
        {
            this.Connect();
            return this.menuDB.GetById(this.MenuID);
        }

        /* Connects to handler, only once per object
         * (required for the cases when object is passed from client)
         */
        public void Connect() {
            if (this.restaurantMenuDB == null) {
                this.restaurantMenuDB = new RestaurantMenuDB();
            }
            if (this.restaurantDB == null) {
                this.restaurantDB = new RestaurantDB();
            }
            if (this.menuDB == null) {
                this.menuDB = new MenuDB();
            }


        }

        /* Adds restaurantMenu to db */
        public Response<RestaurantMenu> Create() {
            this.Connect();
            return this.restaurantMenuDB.Create(this);
        }

        /* Updates restaurantMenu in database */
        public Response<RestaurantMenu> Update() {
            this.Connect();
            return this.restaurantMenuDB.Update(this);
        }

        /*Deletes restaurantMenu from database
         * add here manual cascades, if are needed
         */
        public Response<RestaurantMenu> Delete() {
            this.Connect();
            return this.restaurantMenuDB.Delete(this);
        }

    }

    /* Communicates object to database */
    public class RestaurantMenuDB : SqlHandler<RestaurantMenu> {

        /* translate data from c# to sql */
        private SqlData SetData(RestaurantMenu i) {
            SqlData data = new SqlData();
            //tell translator to which db rows which data belongs
            //data.Set("ID", i.ID);
            data.Set("MenuID", i.Menu.ID);
            data.Set("RestaurantID", i.Restaurant.ID);
            data.Set("Position", i.Position);
            return data;
        }

        #region Validation

        /* possible validations */
        enum Input {
            IdIsNull,
            MenuIsNull,
            RestaurantIsNull,
            MenuIdIsNull,
            RestaurantIdIsNull,
            PositionIsNull
        }

        /* Validate data stored in the object */
        private int Validate(RestaurantMenu restaurantMenu, params Input[] inputs) {
            /* start counting errors
             * Response stores all the information about transaction,
             * also those that is positive 
             */
            int err = 0;

            /* the object is initiated in handler, need to refresh it with each transaction */
            this.Response = new Response<RestaurantMenu>();

            /* first check that the object is not null, 
             * will make it easier to catch other errors with transaction 
             */
            if (restaurantMenu == null) {
                this.Response.AddMessage(ResponseMessage.NullObject); // add message
                err++; // count errors up
            } else {
                /* if object is not null, check all the fields requiring validation */
                /* take input one by one and do a check on the related field(s)
                 * sometimes more complicated than trivial checks are needed.
                 * To keep the code readable, take the check itself outside the scope
                 * */
                foreach (Input input in inputs) {
                    switch (input) {
                        case Input.IdIsNull:
                            if (this.ValidateIdIsNull(restaurantMenu)) {
                                this.Response.AddMessage(ResponseMessage.DataEmpty); // add message
                                err++; // count errors up
                            }
                            break;
                    }
                }
            }
            return err; //return the total number of errors
        }

        #region Raw validation checks
        /* Raw checks for validity on each property requiring validation */
        private bool ValidateIdIsNull(RestaurantMenu restaurantMenu) {
            return (restaurantMenu.ID == null || restaurantMenu.ID == 0);
        }
        #endregion
        #endregion

        public Response<RestaurantMenu> Update(RestaurantMenu restaurantMenu) {
            /* run validation
             * check that id and name are filled up)
             * */
            int err = this.Validate(restaurantMenu, Input.IdIsNull, Input.MenuIdIsNull,
                Input.PositionIsNull,Input.RestaurantIdIsNull, 
                Input.MenuIsNull, Input.RestaurantIsNull);
            // if both fields are filled up, try to update the restaurantMenu 
            if (err < 1) {
                SqlData data = this.SetData(restaurantMenu); // translate c# to Sql
                this.Update(data, restaurantMenu.ID); // run transaction
                /* the results of the transaction are stored 
                 * in this.Response including the SQL mesages and other custom notifications
                 * */

            }

            /* finish by adding the final restaurantMenu to response, 
             * (validation may correct some data without giving a visible to user error)
             * so we can use both the restaurantMenu and the messages later
             * */
            this.Response.Item = restaurantMenu;

            /*return all info we have about this transaction*/
            return this.Response;

        }
        public Response<RestaurantMenu> Delete(RestaurantMenu restaurantMenu) {
            /* relete method does not set a success on response 
             * (I dont remember why! Probably has something to do with batch and chain deletes)
             * rows affected can be zero However this.Success indicated there was no
             * exception thrown by SQL during transaction.
             * ToDo: Have a closer look at whats going on there
             * */

            int err = this.Validate(restaurantMenu, Input.IdIsNull);
            if (err < 1) {
                int rowsAffected = this.Delete(restaurantMenu.ID); // get rows deleted

                /* the following is not really required,
                 * however is good to have for full feedback
                 * */
                if (this.Success) {
                    this.Response.AddMessage(ResponseMessage.DeleteSuccess, rowsAffected.ToString());
                } else {
                    this.Response.AddMessage(ResponseMessage.DeleteHandlerError);
                }
            }
            return this.Response;

        }
        public Response<RestaurantMenu> Create(RestaurantMenu restaurantMenu) {
            //same procedure as update, just dont need id validation
            int err = this.Validate(restaurantMenu, Input.MenuIdIsNull, Input.PositionIsNull, Input.RestaurantIdIsNull,
                Input.MenuIsNull, Input.RestaurantIsNull);


            if (err < 1) {
                SqlData data = this.SetData(restaurantMenu);
                restaurantMenu.ID = this.InsertScopeId(data);
            }

            this.Response.Item = restaurantMenu;
            return this.Response;

        }

        public RestaurantMenu GetById(int id) {
            return this.GetAll().FirstOrDefault(x => x.ID == id);
        }
        public List<Menu> GetMenuesByRestaurantId(int restaurantId) {
            List<RestaurantMenu> rml = this.GetAll().Where(x => x.Restaurant.ID == restaurantId).OrderBy(x => x.Position).ToList();
            List<Menu> menues = new List<Menu>();
            foreach(RestaurantMenu rm in rml)
            {
                menues.Add(rm.Menu);
            }
            return menues;
        }

    }
}
