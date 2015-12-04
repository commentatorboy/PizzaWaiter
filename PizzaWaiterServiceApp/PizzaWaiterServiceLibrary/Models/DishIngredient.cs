using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomHandlers.DatabaseLibrary;
using System.Runtime.Serialization;

namespace Models
{

    /* Describes an object */
    [DataContract]
    public class DishIngredient : SqlModel
    {

        private DishIngredientDB dishIngredientDB; //related DB handler (required)

        private DishDB dishDB;
        private IngredientDB menuDB;

        /*Object properties (custom)*/
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int IngredientID { get; set; }
        [DataMember]
        public int DishID { get; set; }
        [DataMember]
        public Ingredient Ingredient
        {
            get
            {
                return this.GetIngredient();
                //return null;
            }
            set
            {
            }
        }
        [DataMember]
        public Dish Dish
        {
            get
            {
                return this.GetDish();
                //return null;
            }
            set
            {
            }
        }

        public DishIngredient()
        {
            Connect();
        }


        /*Build Object (required)*/
        public void BuildObject(DataRow row)
        {
            this.ID = SqlFormat.ToInt(row, "ID");
            this.IngredientID = SqlFormat.ToInt(row, "IngredientID");
            this.DishID = SqlFormat.ToInt(row, "DishID");

            //this.Connect();
            //this.Ingredient = menuDB.GetById(SqlFormat.ToInt(row, "IngredientID"));
            //this.Dish = dishDB.GetById(SqlFormat.ToInt(row, "DishID"));

        }

        private Ingredient GetIngredient()
        {
            //this.Connect();
            return this.menuDB.GetById(this.IngredientID);
        }
        private Dish GetDish()
        {
            //this.Connect();
            return this.dishDB.GetById(this.DishID);
        }

        /* Connects to handler, only once per object
         * (required for the cases when object is passed from client)
         */
        public void Connect()
        {
            if (this.dishIngredientDB == null)
            {
                this.dishIngredientDB = new DishIngredientDB();
            }
            if (this.dishDB == null)
            {
                this.dishDB = new DishDB();
            }
        }

        /* Adds dishIngredient to db */
        public Response<DishIngredient> Create()
        {
            this.Connect();
            return this.dishIngredientDB.Create(this);
        }

        /* Updates dishIngredient in database */
        public Response<DishIngredient> Update()
        {
            this.Connect();
            return this.dishIngredientDB.Update(this);
        }

        /*Deletes dishIngredient from database
         * add here manual cascades, if are needed
         */
        public Response<DishIngredient> Delete()
        {
            this.Connect();
            return this.dishIngredientDB.Delete(this);
        }

    }

    /* Communicates object to database */
    public class DishIngredientDB : SqlHandler<DishIngredient>
    {

        /* translate data from c# to sql */
        private SqlData SetData(DishIngredient i)
        {
            SqlData data = new SqlData();
            //tell translator to which db rows which data belongs
            //data.Set("ID", i.ID);
            data.Set("IngredientID", i.Ingredient.ID);
            data.Set("DishID", i.Dish.ID);
            return data;
        }

        #region Validation

        /* possible validations */
        enum Input
        {
            IdIsNull,
            IngredientIsNull,
            DishIsNull
        }

        /* Validate data stored in the object */
        private int Validate(DishIngredient dishIngredient, params Input[] inputs)
        {
            /* start counting errors
             * Response stores all the information about transaction,
             * also those that is positive 
             */
            int err = 0;

            /* the object is initiated in handler, need to refresh it with each transaction */
            this.Response = new Response<DishIngredient>();

            /* first check that the object is not null, 
             * will make it easier to catch other errors with transaction 
             */
            if (dishIngredient == null)
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
                            if (this.ValidateIdIsNull(dishIngredient))
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
        private bool ValidateIdIsNull(DishIngredient dishIngredient)
        {
            return (dishIngredient.ID == null || dishIngredient.ID == 0);
        }
        #endregion
        #endregion

        public Response<DishIngredient> Update(DishIngredient dishIngredient)
        {
            /* run validation
             * check that id and name are filled up)
             * */
            int err = this.Validate(dishIngredient, Input.IdIsNull,
                Input.IngredientIsNull, Input.DishIsNull);
            // if both fields are filled up, try to update the dishIngredient 
            if (err < 1)
            {
                SqlData data = this.SetData(dishIngredient); // translate c# to Sql
                this.Update(data, dishIngredient.ID); // run transaction
                /* the results of the transaction are stored 
                 * in this.Response including the SQL mesages and other custom notifications
                 * */

            }

            /* finish by adding the final dishIngredient to response, 
             * (validation may correct some data without giving a visible to user error)
             * so we can use both the dishIngredient and the messages later
             * */
            this.Response.Item = dishIngredient;

            /*return all info we have about this transaction*/
            return this.Response;

        }
        public Response<DishIngredient> Delete(DishIngredient dishIngredient)
        {
            /* relete method does not set a success on response 
             * (I dont remember why! Probably has something to do with batch and chain deletes)
             * rows affected can be zero However this.Success indicated there was no
             * exception thrown by SQL during transaction.
             * ToDo: Have a closer look at whats going on there
             * */

            int err = this.Validate(dishIngredient, Input.IdIsNull);
            if (err < 1)
            {
                int rowsAffected = this.Delete(dishIngredient.ID); // get rows deleted

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
        public Response<DishIngredient> Create(DishIngredient dishIngredient)
        {
            //same procedure as update, just dont need id validation
            int err = this.Validate(dishIngredient, Input.IngredientIsNull, Input.DishIsNull);


            if (err < 1)
            {
                SqlData data = this.SetData(dishIngredient);
                dishIngredient.ID = this.InsertScopeId(data);
            }

            this.Response.Item = dishIngredient;
            return this.Response;

        }

        public DishIngredient GetById(int id)
        {
            return this.GetAll().FirstOrDefault(x => x.ID == id);
        }
        public List<DishIngredient> GetByDishId(int dishId)
        {
            return this.GetAll().Where(x => x.Dish.ID == dishId).ToList();
        }

    }
}
