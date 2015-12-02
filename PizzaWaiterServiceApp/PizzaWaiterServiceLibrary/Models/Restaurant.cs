using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CustomHandlers.DatabaseLibrary;

namespace Models {

    /* Describes an object */
    [DataContract]
    public class Restaurant : SqlModel {

        private RestaurantDB restaurantDB; //related DB handler (required)
        private RestaurantMenuDB restaurantMenuDB;

        /*Object properties (custom)*/
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public List<Menu> Menues
        {
            get
            {
                return GetMenues();
            }
            set
            {
                /// TODO: Order it by Position
            }
        }

        private List<Menu> GetMenues()
        {
            this.Connect();
            return this.restaurantMenuDB.GetMenuesByRestaurantId(this.ID);
        }


        /*Build Object (required)*/
        public void BuildObject(DataRow row) {
            this.ID = SqlFormat.ToInt(row, "ID");
            this.Name = SqlFormat.ToString(row, "Name");

            //this.Connect();
            //this.RestaurantMenues = this.restaurantMenuDB.GetByRestaurantID(this.ID);
            

        }



        /* Connects to handler, only once per object
         * (required for the cases when object is passed from client)
         */
        public void Connect() {
            if (this.restaurantDB == null) {
                this.restaurantDB = new RestaurantDB();
            }
            if (this.restaurantMenuDB == null) {
                this.restaurantMenuDB = new RestaurantMenuDB();
            }

        }

        /* Adds restaurant to db */
        public Response<Restaurant> Create() {
            this.Connect();
            return this.restaurantDB.Create(this);
        }

        /* Updates restaurant in database */
        public Response<Restaurant> Update() {
            this.Connect();
            return this.restaurantDB.Update(this);
        }

        /*Deletes restaurant from database
         * add here manual cascades, if are needed
         */
        public Response<Restaurant> Delete() {
            this.Connect();
            return this.restaurantDB.Delete(this);
        }

    }

    /* Communicates object to database */
    public class RestaurantDB : SqlHandler<Restaurant> {

        /* translate data from c# to sql */
        private SqlData SetData(Restaurant i) {
            SqlData data = new SqlData();
            //tell translator to which db rows which data belongs
            //data.Set("ID", i.ID);
            data.Set("Name", i.Name);

            return data;
        }

        #region Validation

        /* possible validations */
        enum Input {
            IdIsNull,
            NameIsNull
        }

        /* Validate data stored in the object */
        private int Validate(Restaurant restaurant, params Input[] inputs) {
            /* start counting errors
             * Response stores all the information about transaction,
             * also those that is positive 
             */
            int err = 0;

            /* the object is initiated in handler, need to refresh it with each transaction */
            this.Response = new Response<Restaurant>();

            /* first check that the object is not null, 
             * will make it easier to catch other errors with transaction 
             */
            if (restaurant == null) {
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
                            if (this.ValidateIdIsNull(restaurant)) {
                                this.Response.AddMessage(ResponseMessage.DataEmpty); // add message
                                err++; // count errors up
                            }
                            break;
                        /*
                    case Input.TitleIsNull:
                        if (this.ValidateTitleIsNull(restaurant)) {
                            this.Response.AddMessage(ResponseMessage.DataEmpty); // add message
                            err++; // count errors up
                        }
                        break;
                         * */
                    }
                }
            }
            return err; //return the total number of errors
        }

        #region Raw validation checks
        /* Raw checks for validity on each property requiring validation */
        private bool ValidateIdIsNull(Restaurant restaurant) {
            return (restaurant.ID == null || restaurant.ID == 0);
        }
        /*
        private bool ValidateTitleIsNull(Restaurant restaurant) {
            return (restaurant.Title == null || restaurant.Title == "");
        }*/
        #endregion
        #endregion

        public Response<Restaurant> Update(Restaurant restaurant) {
            /* run validation
             * check that id and name are filled up)
             * */
            int err = this.Validate(restaurant, Input.IdIsNull, Input.NameIsNull);
            // if both fields are filled up, try to update the restaurant 
            if (err < 1) {
                SqlData data = this.SetData(restaurant); // translate c# to Sql
                this.Update(data, restaurant.ID); // run transaction
                /* the results of the transaction are stored 
                 * in this.Response including the SQL mesages and other custom notifications
                 * */

            }

            /* finish by adding the final restaurant to response, 
             * (validation may correct some data without giving a visible to user error)
             * so we can use both the restaurant and the messages later
             * */
            this.Response.Item = restaurant;

            /*return all info we have about this transaction*/
            return this.Response;

        }
        public Response<Restaurant> Delete(Restaurant restaurant) {
            /* relete method does not set a success on response 
             * (I dont remember why! Probably has something to do with batch and chain deletes)
             * rows affected can be zero However this.Success indicated there was no
             * exception thrown by SQL during transaction.
             * ToDo: Have a closer look at whats going on there
             * */

            int err = this.Validate(restaurant, Input.IdIsNull);
            if (err < 1) {
                int rowsAffected = this.Delete(restaurant.ID); // get rows deleted

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
        public Response<Restaurant> Create(Restaurant restaurant) {
            //same procedure as update, just dont need id validation
            int err = this.Validate(restaurant, Input.NameIsNull);


            if (err < 1) {
                SqlData data = this.SetData(restaurant);
                restaurant.ID = this.InsertScopeId(data);
            }

            this.Response.Item = restaurant;
            return this.Response;

        }

        public Restaurant GetById(int id) {
            //return this.GetAll().FirstOrDefault(x => x.ID == id);
            Restaurant r = new Restaurant();
            r.Name = "The best";
            r.ID = 1;
            return r;
        }
    }
}
