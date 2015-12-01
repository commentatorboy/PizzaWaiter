using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomHandlers.DatabaseLibrary;

namespace Models {

    /* Describes an object */
    public class Order : SqlModel {

        private OrderDB orderDB; //related DB handler (required)

        /*Object properties (custom)*/
        public int ID { get; set; }
        public int UserID { get; set; }
        public int AddressID { get; set; }
        public int StatusID { get; set; }

        /*Build Object (required)*/
        public void BuildObject(DataRow row) {
            this.ID = SqlFormat.ToInt(row, "ID");
            this.UserID = SqlFormat.ToInt(row,"UserID");
            this.AddressID = SqlFormat.ToInt(row, "AddressID");
            this.StatusID = SqlFormat.ToInt(row, "StatusID");

        }

        /* Connects to handler, only once per object
         * (required for the cases when object is passed from client)
         */
        public void Connect() {
            if (this.orderDB == null) {
                this.orderDB = new OrderDB();
            }

        }

        /* Adds order to db */
        public Response<Order> Create() {
            this.Connect();
            return this.orderDB.Create(this);
        }

        /* Updates order in database */
        public Response<Order> Update() {
            this.Connect();
            return this.orderDB.Update(this);
        }

        /*Deletes order from database
         * add here manual cascades, if are needed
         */
        public Response<Order> Delete() {
            this.Connect();
            return this.orderDB.Delete(this);
        }

    }

    /* Communicates object to database */
    public class OrderDB : SqlHandler<Order> {

        /* translate data from c# to sql */
        private SqlData SetData(Order i) {
            SqlData data = new SqlData();
            //tell translator to which db rows which data belongs
            //data.Set("ID", i.ID);
            data.Set("UserID", i.UserID);
            data.Set("AddressID", i.AddressID);
            data.Set("StatusID", i.StatusID);
            return data;
        }

        #region Validation

        /* possible validations */
        enum Input {
            IdIsNull,
            UserIdIsNull,
            AddressIdIsNull,
            StatusIdIsNull
        }

        /* Validate data stored in the object */
        private int Validate(Order order, params Input[] inputs) {
            /* start counting errors
             * Response stores all the information about transaction,
             * also those that is positive 
             */
            int err = 0;

            /* the object is initiated in handler, need to refresh it with each transaction */
            this.Response = new Response<Order>();

            /* first check that the object is not null, 
             * will make it easier to catch other errors with transaction 
             */
            if (order == null) {
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
                            if (this.ValidateIdIsNull(order)) {
                                this.Response.AddMessage(ResponseMessage.DataEmpty); // add message
                                err++; // count errors up
                            }
                            break;
                            /*
                        case Input.TitleIsNull:
                            if (this.ValidateTitleIsNull(order)) {
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
        private bool ValidateIdIsNull(Order order) {
            return (order.ID == null || order.ID == 0);
        }
        /*
        private bool ValidateTitleIsNull(Order order) {
            return (order.Title == null || order.Title == "");
        }*/
        #endregion
        #endregion

        public Response<Order> Update(Order order) {
            /* run validation
             * check that id and name are filled up)
             * */
            int err = this.Validate(order, Input.IdIsNull, Input.AddressIdIsNull,Input.StatusIdIsNull,Input.UserIdIsNull);
            // if both fields are filled up, try to update the order 
            if (err < 1) {
                SqlData data = this.SetData(order); // translate c# to Sql
                this.Update(data, order.ID); // run transaction
                /* the results of the transaction are stored 
                 * in this.Response including the SQL mesages and other custom notifications
                 * */

            }

            /* finish by adding the final order to response, 
             * (validation may correct some data without giving a visible to user error)
             * so we can use both the order and the messages later
             * */
            this.Response.Item = order;

            /*return all info we have about this transaction*/
            return this.Response;

        }
        public Response<Order> Delete(Order order) {
            /* relete method does not set a success on response 
             * (I dont remember why! Probably has something to do with batch and chain deletes)
             * rows affected can be zero However this.Success indicated there was no
             * exception thrown by SQL during transaction.
             * ToDo: Have a closer look at whats going on there
             * */

            int err = this.Validate(order, Input.IdIsNull);
            if (err < 1) {
                int rowsAffected = this.Delete(order.ID); // get rows deleted

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
        public Response<Order> Create(Order order) {
            //same procedure as update, just dont need id validation
            int err = this.Validate(order, Input.AddressIdIsNull, Input.StatusIdIsNull, Input.UserIdIsNull);


            if (err < 1) {
                SqlData data = this.SetData(order);
                order.ID = this.InsertScopeId(data);
            }

            this.Response.Item = order;
            return this.Response;

        }

        public Order GetById(int id) {
            return this.GetAll().FirstOrDefault(x => x.ID == id);
        }
    }
}
