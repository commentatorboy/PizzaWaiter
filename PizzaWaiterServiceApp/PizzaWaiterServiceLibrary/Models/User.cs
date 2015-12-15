using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomHandlers.DatabaseLibrary;

namespace Models {

    /* Describes an object */
    public class User : SqlModel {

        private UserDB userDB; //related DB handler (required)

        /*Object properties (custom)*/
        public int ID { get; set; }
        public String PhoneNumber { get; set; }
        public int RankID { get; set; }

        /*Build Object (required)*/
        public void BuildObject(DataRow row) {
            this.ID = SqlFormat.ToInt(row, "ID");
            this.PhoneNumber = SqlFormat.ToString(row, "PhoneNumber");
            this.RankID = SqlFormat.ToInt(row, "RankID");


        }

        /* Connects to handler, only once per object
         * (required for the cases when object is passed from client)
         */
        public void Connect() {
            if (this.userDB == null) {
                this.userDB = new UserDB();
            }

        }

        /* Adds user to db */
        public Response<User> Create() {
            this.Connect();
            return this.userDB.Create(this);
        }

        /* Updates user in database */
        public Response<User> Update() {
            this.Connect();
            return this.userDB.Update(this);
        }

        /*Deletes user from database
         * add here manual cascades, if are needed
         */
        public Response<User> Delete() {
            this.Connect();
            return this.userDB.Delete(this);
        }

    }

    /* Communicates object to database */
    public class UserDB : SqlHandler<User> {

        /* translate data from c# to sql */
        private SqlData SetData(User i) {
            SqlData data = new SqlData();
            //tell translator to which db rows which data belongs
            //data.Set("ID", i.ID);
            data.Set("PhoneNumber", i.PhoneNumber);
            data.Set("RankID", i.RankID);
            return data;
        }

        #region Validation

        /* possible validations */
        enum Input {
            IdIsNull,
            PhoneNumberIsNull,
            RankIdIsNull
        }

        /* Validate data stored in the object */
        private int Validate(User user, params Input[] inputs) {
            /* start counting errors
             * Response stores all the information about transaction,
             * also those that is positive 
             */
            int err = 0;

            /* the object is initiated in handler, need to refresh it with each transaction */
            this.Response = new Response<User>();

            /* first check that the object is not null, 
             * will make it easier to catch other errors with transaction 
             */
            if (user == null) {
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
                            if (this.ValidateIdIsNull(user)) {
                                this.Response.AddMessage(ResponseMessage.DataEmpty); // add message
                                err++; // count errors up
                            }
                            break;
                        /*
                    case Input.TitleIsNull:
                        if (this.ValidateTitleIsNull(user)) {
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
        private bool ValidateIdIsNull(User user) {
            return (user.ID == null || user.ID == 0);
        }
        /*
        private bool ValidateTitleIsNull(User user) {
            return (user.Title == null || user.Title == "");
        }*/
        #endregion
        #endregion

        public Response<User> Update(User user) {
            /* run validation
             * check that id and name are filled up)
             * */
            int err = this.Validate(user, Input.IdIsNull, Input.PhoneNumberIsNull,Input.RankIdIsNull);
            // if both fields are filled up, try to update the user 
            if (err < 1) {
                SqlData data = this.SetData(user); // translate c# to Sql
                this.Update(data, user.ID); // run transaction
                /* the results of the transaction are stored 
                 * in this.Response including the SQL mesages and other custom notifications
                 * */

            }
            if(this.Response.Messages.Count == 0)
            {
                this.Response.Success = true;
            }
            /* finish by adding the final user to response, 
             * (validation may correct some data without giving a visible to user error)
             * so we can use both the user and the messages later
             * */
            this.Response.Item = user;

            /*return all info we have about this transaction*/
            return this.Response;

        }
        public Response<User> Delete(User user) {
            /* relete method does not set a success on response 
             * (I dont remember why! Probably has something to do with batch and chain deletes)
             * rows affected can be zero However this.Success indicated there was no
             * exception thrown by SQL during transaction.
             * ToDo: Have a closer look at whats going on there
             * */

            int err = this.Validate(user, Input.IdIsNull);
            if (err < 1) {
                int rowsAffected = this.Delete(user.ID); // get rows deleted

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
        public Response<User> Create(User user) {
            //same procedure as update, just dont need id validation
            int err = this.Validate(user, Input.PhoneNumberIsNull, Input.RankIdIsNull);


            if (err < 1) {
                SqlData data = this.SetData(user);
                user.ID = this.InsertScopeId(data);
            }

            this.Response.Item = user;
            return this.Response;

        }

        public User GetById(int id) {
            return this.GetAll().FirstOrDefault(x => x.ID == id);
        }

        internal User GetUserByPhone(string phone)
        {
            return this.GetAll().FirstOrDefault(x => x.PhoneNumber == phone);
        }
    }
}
