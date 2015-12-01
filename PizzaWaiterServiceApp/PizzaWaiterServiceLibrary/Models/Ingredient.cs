using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomHandlers.DatabaseLibrary;

namespace Models
{
    /* Describes an object */
    public class Ingredient : SqlModel
    {

        private IngredientDB dishDB; //related DB handler (required)

        /*Object properties (custom)*/
        public int ID { get; set; }
        public string Name { get; set; }

        /*Build Object (required)*/
        public void BuildObject(DataRow row)
        {
            this.ID = SqlFormat.ToInt(row, "ID");
            this.Name = SqlFormat.ToString(row, "Name");
        }

        /* Connects to handler, only once per object
         * (required for the cases when object is passed from client)
         */
        public void Connect()
        {
            if (this.dishDB == null)
            {
                this.dishDB = new IngredientDB();
            }

        }

        /* Adds dish to db */
        public Response<Ingredient> Create()
        {
            this.Connect();
            return this.dishDB.Create(this);
        }

        /* Updates dish in database */
        public Response<Ingredient> Update()
        {
            this.Connect();
            return this.dishDB.Update(this);
        }

        /*Deletes dish from database
         * add here manual cascades, if are needed
         */
        public Response<Ingredient> Delete()
        {
            this.Connect();
            return this.dishDB.Delete(this);
        }

    }

    /* Communicates object to database */
    public class IngredientDB : SqlHandler<Ingredient>
    {

        /* translate data from c# to sql */
        private SqlData SetData(Ingredient i)
        {
            SqlData data = new SqlData();
            //tell translator to which db rows which data belongs
            //data.Set("ID", i.ID);
            data.Set("Name", i.Name);
            return data;
        }

        #region Validation

        /* possible validations */
        enum Input
        {
            IdIsNull,
            OrderIdIsNull,
            IngredientIdIsNull,
            NumberIsNull
        }

        /* Validate data stored in the object */
        private int Validate(Ingredient dish, params Input[] inputs)
        {
            /* start counting errors
             * Response stores all the information about transaction,
             * also those that is positive 
             */
            int err = 0;

            /* the object is initiated in handler, need to refresh it with each transaction */
            this.Response = new Response<Ingredient>();

            /* first check that the object is not null, 
             * will make it easier to catch other errors with transaction 
             */
            if (dish == null)
            {
                this.Response.AddMessage(ResponseMessage.NullObject); // add message
                err++; // count errors up
            }
            else
            {
                /* if object is not null, check all the fields requiring validation */
                /* take input one by one and do a check on the related field(s)
                 * sometimes more complicated than trivial checks are needed.
                 * To keep the code readable, take the check itself outside the scope
                 * */
                foreach (Input input in inputs)
                {
                    switch (input)
                    {
                        case Input.IdIsNull:
                            if (this.ValidateIdIsNull(dish))
                            {
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
        private bool ValidateIdIsNull(Ingredient dish)
        {
            return (dish.ID == null || dish.ID == 0);
        }
        private bool ValidateOrderIdIsNull(Ingredient dish)
        {
            return (dish.Name == null || dish.Name == "");
        }
        #endregion
        #endregion

        public Response<Ingredient> Update(Ingredient dish)
        {
            /* run validation
             * check that id and name are filled up)
             * */
            int err = this.Validate(dish, Input.IdIsNull, Input.NumberIsNull, Input.IngredientIdIsNull, Input.OrderIdIsNull);
            // if both fields are filled up, try to update the dish 
            if (err < 1)
            {
                SqlData data = this.SetData(dish); // translate c# to Sql
                this.Update(data, dish.ID); // run transaction
                                            /* the results of the transaction are stored 
                                             * in this.Response including the SQL mesages and other custom notifications
                                             * */

            }

            /* finish by adding the final dish to response, 
             * (validation may correct some data without giving a visible to user error)
             * so we can use both the dish and the messages later
             * */
            this.Response.Item = dish;

            /*return all info we have about this transaction*/
            return this.Response;

        }
        public Response<Ingredient> Delete(Ingredient dish)
        {
            /* relete method does not set a success on response 
             * (I dont remember why! Probably has something to do with batch and chain deletes)
             * rows affected can be zero However this.Success indicated there was no
             * exception thrown by SQL during transaction.
             * ToDo: Have a closer look at whats going on there
             * */

            int err = this.Validate(dish, Input.IdIsNull);
            if (err < 1)
            {
                int rowsAffected = this.Delete(dish.ID); // get rows deleted

                /* the following is not really required,
                 * however is good to have for full feedback
                 * */
                if (this.Success)
                {
                    this.Response.AddMessage(ResponseMessage.DeleteSuccess, rowsAffected.ToString());
                }
                else
                {
                    this.Response.AddMessage(ResponseMessage.DeleteHandlerError);
                }
            }
            return this.Response;

        }
        public Response<Ingredient> Create(Ingredient dish)
        {
            //same procedure as update, just dont need id validation
            int err = this.Validate(dish, Input.NumberIsNull, Input.IngredientIdIsNull, Input.OrderIdIsNull);


            if (err < 1)
            {
                SqlData data = this.SetData(dish);
                dish.ID = this.InsertScopeId(data);
            }

            this.Response.Item = dish;
            return this.Response;

        }

        public Ingredient GetById(int id)
        {
            return this.GetAll().FirstOrDefault(x => x.ID == id);
        }
    }
}
