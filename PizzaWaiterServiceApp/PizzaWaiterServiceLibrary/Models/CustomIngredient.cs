using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomHandlers.DatabaseLibrary;

namespace Models {

    /* Describes an object */
    public class CustomIngredient : SqlModel {

        private CustomIngredientDB customIngredientDB; //related DB handler (required)

        /*Object properties (custom)*/
        public int ID { get; set; }
        public int PartOrderID { get; set; }
        public int IngredientID { get; set; }
        public Boolean Include { get; set; }

        /*Build Object (required)*/
        public void BuildObject(DataRow row) {
            this.ID = SqlFormat.ToInt(row, "ID");
            this.PartOrderID = SqlFormat.ToInt(row, "PartOrderID");
            this.IngredientID = SqlFormat.ToInt(row, "IngredientID");
            this.Include = SqlFormat.ToBool(row, "Include");


        }

        /* Connects to handler, only once per object
         * (required for the cases when object is passed from client)
         */
        public void Connect() {
            if (this.customIngredientDB == null) {
                this.customIngredientDB = new CustomIngredientDB();
            }

        }

        /* Adds customIngredient to db */
        public Response<CustomIngredient> Create() {
            this.Connect();
            return this.customIngredientDB.Create(this);
        }

        /* Updates customIngredient in database */
        public Response<CustomIngredient> Update() {
            this.Connect();
            return this.customIngredientDB.Update(this);
        }

        /*Deletes customIngredient from database
         * add here manual cascades, if are needed
         */
        public Response<CustomIngredient> Delete() {
            this.Connect();
            return this.customIngredientDB.Delete(this);
        }

    }

    /* Communicates object to database */
    public class CustomIngredientDB : SqlHandler<CustomIngredient> {

        /* translate data from c# to sql */
        private SqlData SetData(CustomIngredient i) {
            SqlData data = new SqlData();
            //tell translator to which db rows which data belongs
            //data.Set("ID", i.ID);
            data.Set("PartOrderID", i.PartOrderID);
            data.Set("IngredientID", i.IngredientID);
            data.Set("Include", i.Include);

            return data;
        }

        #region Validation

        /* possible validations */
        enum Input {
            IdIsNull,
            PartOrderIdIsNull,
            IngredientIdIsNull,
            IncludeIsNull
        }

        /* Validate data stored in the object */
        private int Validate(CustomIngredient customIngredient, params Input[] inputs) {
            /* start counting errors
             * Response stores all the information about transaction,
             * also those that is positive 
             */
            int err = 0;

            /* the object is initiated in handler, need to refresh it with each transaction */
            this.Response = new Response<CustomIngredient>();

            /* first check that the object is not null, 
             * will make it easier to catch other errors with transaction 
             */
            if (customIngredient == null) {
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
                            if (this.ValidateIdIsNull(customIngredient)) {
                                this.Response.AddMessage(ResponseMessage.DataEmpty); // add message
                                err++; // count errors up
                            }
                            break;
                        /*
                    case Input.TitleIsNull:
                        if (this.ValidateTitleIsNull(customIngredient)) {
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
        private bool ValidateIdIsNull(CustomIngredient customIngredient) {
            return (customIngredient.ID == null || customIngredient.ID == 0);
        }
        /*
        private bool ValidateTitleIsNull(CustomIngredient customIngredient) {
            return (customIngredient.Title == null || customIngredient.Title == "");
        }*/
        #endregion
        #endregion

        public Response<CustomIngredient> Update(CustomIngredient customIngredient) {
            /* run validation
             * check that id and name are filled up)
             * */
            int err = this.Validate(customIngredient, Input.IdIsNull, Input.IncludeIsNull,Input.IngredientIdIsNull,Input.PartOrderIdIsNull);
            // if both fields are filled up, try to update the customIngredient 
            if (err < 1) {
                SqlData data = this.SetData(customIngredient); // translate c# to Sql
                this.Update(data, customIngredient.ID); // run transaction
                /* the results of the transaction are stored 
                 * in this.Response including the SQL mesages and other custom notifications
                 * */

            }

            /* finish by adding the final customIngredient to response, 
             * (validation may correct some data without giving a visible to user error)
             * so we can use both the customIngredient and the messages later
             * */
            this.Response.Item = customIngredient;

            /*return all info we have about this transaction*/
            return this.Response;

        }
        public Response<CustomIngredient> Delete(CustomIngredient customIngredient) {
            /* relete method does not set a success on response 
             * (I dont remember why! Probably has something to do with batch and chain deletes)
             * rows affected can be zero However this.Success indicated there was no
             * exception thrown by SQL during transaction.
             * ToDo: Have a closer look at whats going on there
             * */

            int err = this.Validate(customIngredient, Input.IdIsNull);
            if (err < 1) {
                int rowsAffected = this.Delete(customIngredient.ID); // get rows deleted

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
        public Response<CustomIngredient> DeleteBatchByPartOrderList(List<PartOrder> partOrders)
        {
            ///stub: 
            this.Response = new Response<CustomIngredient>();
            //make sure that the partOrders is not null.
            if(partOrders != null)
            {
                string[] completeString = new string[partOrders.Count];
                int var = 0;

                foreach (PartOrder po in partOrders)
                {
                    ///Delete from (class name) where id=(orderIdn0) OR id=(orderIdn..) OR id=(orderIdn)
                    string joinPartOrdersID = string.Format("PartOrderID = {0}", po.ID);
                    completeString[var] = joinPartOrdersID;
                    var++;
                }

                string sqlCondition = string.Join(" OR ", completeString);
                

                int rowcount = this.Delete(sqlCondition);
                if (this.Success)
                {
                    this.Response.Messages.Add(string.Format("request successfull. {0} Custom ingredients deleted", rowcount));
                    this.Response.Success = true;
                }
                else
                {
                    this.Response.Messages.Add("Request failed in Custom Ingredient DeleteBatchByPartOrderList");
                }                
            }
            return this.Response;
        }

        public Response<CustomIngredient> Create(CustomIngredient customIngredient) {
            //same procedure as update, just dont need id validation
            int err = this.Validate(customIngredient, Input.IncludeIsNull, Input.IngredientIdIsNull, Input.PartOrderIdIsNull);


            if (err < 1) {
                SqlData data = this.SetData(customIngredient);
                customIngredient.ID = this.InsertScopeId(data);
            }

            this.Response.Item = customIngredient;
            return this.Response;

        }

        public CustomIngredient GetById(int id) {
            return this.GetAll().FirstOrDefault(x => x.ID == id);
        }
    }
}
