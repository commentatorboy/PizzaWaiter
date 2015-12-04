using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CustomHandlers.DatabaseLibrary;

namespace Models
{
    /* Describes an object */
    [DataContract]
    public class PartOrder : SqlModel
    {

        private PartOrderDB partOrderDB; //related DB handler (required)
        private DishDB dishDB;
        private OrderDB orderDB;

        /*Object properties (custom)*/
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int DishID { get; set; }
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public int Amount { get; set; }

        [DataMember]
        public Dish Dish {
            get { return this.dishDB.GetById(this.DishID); }
            set {  }
        }
        [DataMember]
        public Order Order {
            get { return this.orderDB.GetById(this.OrderID); }
            set { }
        }

        [DataMember]
        public List<CustomIngredient> CustomIngredients { get; set; }
        

        public PartOrder() {
            Connect();
        }
        /*Build Object (required)*/
        public void BuildObject(DataRow row)
        {
            this.ID = SqlFormat.ToInt(row, "ID");
            this.DishID = SqlFormat.ToInt(row, "DishID");
            this.OrderID = SqlFormat.ToInt(row, "OrderID");
            this.Amount = SqlFormat.ToInt(row, "Amount");
        }

        /* Connects to handler, only once per object
         * (required for the cases when object is passed from client)
         */
        public void Connect()
        {
            if (this.partOrderDB == null)
            {
                this.partOrderDB = new PartOrderDB();
            }
            if (this.orderDB == null) {
                this.orderDB = new OrderDB();
            }
            if (this.dishDB == null) {
                this.dishDB = new DishDB();
            }

        }

        /* Adds partOrder to db */
        public Response<PartOrder> Create()
        {
            this.Connect();
            return this.partOrderDB.Create(this);
        }

        /* Updates partOrder in database */
        public Response<PartOrder> Update()
        {
            this.Connect();
            return this.partOrderDB.Update(this);
        }

        /*Deletes partOrder from database
         * add here manual cascades, if are needed
         */
        public Response<PartOrder> Delete()
        {
            this.Connect();
            return this.partOrderDB.Delete(this);
        }

    }

    /* Communicates object to database */
    public class PartOrderDB : SqlHandler<PartOrder>
    {

        /* translate data from c# to sql */
        private SqlData SetData(PartOrder i)
        {
            SqlData data = new SqlData();
            //tell translator to which db rows which data belongs
            //data.Set("ID", i.ID);
            data.Set("DishID", i.DishID);
            data.Set("OrderID", i.OrderID);
            data.Set("Amount", i.Amount);
            return data;
        }

        #region Validation

        /* possible validations */
        enum Input
        {
            IdIsNull,
            OrderIdIsNull,
            DishIdIsNull,
            AmountIsNull
        }

        /* Validate data stored in the object */
        private int Validate(PartOrder partOrder, params Input[] inputs)
        {
            /* start counting errors
             * Response stores all the information about transaction,
             * also those that is positive 
             */
            int err = 0;

            /* the object is initiated in handler, need to refresh it with each transaction */
            this.Response = new Response<PartOrder>();

            /* first check that the object is not null, 
             * will make it easier to catch other errors with transaction 
             */
            if (partOrder == null)
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
                            if (this.ValidateIdIsNull(partOrder))
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
        private bool ValidateIdIsNull(PartOrder partOrder)
        {
            return (partOrder.ID == null || partOrder.ID == 0);
        }
        private bool ValidateDishIdIsNull(PartOrder partOrder)
        {
            return (partOrder.DishID == null || partOrder.DishID == 0);
        }
        private bool ValidateOrderIdIsNull(PartOrder partOrder)
        {
            return (partOrder.OrderID == null || partOrder.OrderID == 0);
        }
        private bool ValidateAmountIsNull(PartOrder partOrder)
        {
            return (partOrder.Amount == null || partOrder.Amount == 0);
        }
        #endregion
        #endregion

        public Response<PartOrder> Update(PartOrder partOrder)
        {
            /* run validation
             * check that id and name are filled up)
             * */
            int err = this.Validate(partOrder, Input.IdIsNull, Input.AmountIsNull, Input.DishIdIsNull, Input.OrderIdIsNull);
            // if both fields are filled up, try to update the partOrder 
            if (err < 1)
            {
                SqlData data = this.SetData(partOrder); // translate c# to Sql
                this.Update(data, partOrder.ID); // run transaction
                                            /* the results of the transaction are stored 
                                             * in this.Response including the SQL mesages and other custom notifications
                                             * */

            }

            /* finish by adding the final partOrder to response, 
             * (validation may correct some data without giving a visible to user error)
             * so we can use both the partOrder and the messages later
             * */
            this.Response.Item = partOrder;

            /*return all info we have about this transaction*/
            return this.Response;

        }
        public Response<PartOrder> Delete(PartOrder partOrder)
        {
            /* relete method does not set a success on response 
             * (I dont remember why! Probably has something to do with batch and chain deletes)
             * rows affected can be zero However this.Success indicated there was no
             * exception thrown by SQL during transaction.
             * ToDo: Have a closer look at whats going on there
             * */

            int err = this.Validate(partOrder, Input.IdIsNull);
            if (err < 1)
            {
                int rowsAffected = this.Delete(partOrder.ID); // get rows deleted

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
        public Response<PartOrder> Create(PartOrder partOrder)
        {
            //same procedure as update, just dont need id validation
            int err = this.Validate(partOrder, Input.AmountIsNull, Input.DishIdIsNull, Input.OrderIdIsNull);


            if (err < 1)
            {
                SqlData data = this.SetData(partOrder);
                partOrder.ID = this.InsertScopeId(data);
            }

            this.Response.Item = partOrder;
            return this.Response;

        }

        public PartOrder GetById(int id)
        {
            return this.GetAll().FirstOrDefault(x => x.ID == id);
        }
    }

    
}
