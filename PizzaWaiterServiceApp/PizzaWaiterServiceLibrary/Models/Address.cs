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
    public class Address : SqlModel
    {

        private AddressDB addressDB; //related DB handler (required)

        /*Object properties (custom)*/
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserAddress { get; set; }

        /*Build Object (required)*/
        public void BuildObject(DataRow row)
        {
            this.ID = SqlFormat.ToInt(row, "ID");
            this.UserID = SqlFormat.ToInt(row, "UserID");
            this.UserAddress = SqlFormat.ToString(row, "UserAddress");
        }

        /* Connects to handler, only once per object
            * (required for the cases when object is passed from client)
            */
        public void Connect()
        {
            if (this.addressDB == null)
            {
                this.addressDB = new AddressDB();
            }

        }

        /* Adds address to db */
        public Response<Address> Create()
        {
            this.Connect();
            return this.addressDB.Create(this);
        }

        /* Updates address in database */
        public Response<Address> Update()
        {
            this.Connect();
            return this.addressDB.Update(this);
        }

        /*Deletes address from database
            * add here manual cascades, if are needed
            */
        public Response<Address> Delete()
        {
            this.Connect();
            return this.addressDB.Delete(this);
        }

    }

    /* Communicates object to database */
    public class AddressDB : SqlHandler<Address>
    {

        /* translate data from c# to sql */
        private SqlData SetData(Address i)
        {
            SqlData data = new SqlData();
            //tell translator to which db rows which data belongs
            //data.Set("ID", i.ID);
            data.Set("UserID", i.UserID);
            data.Set("UserAddress", i.UserAddress);
            return data;
        }

        #region Validation

        /* possible validations */
        enum Input
        {
            IdIsNull,
            UserIdIsNull,
            UserAddressIsNull
        }

        /* Validate data stored in the object */
        private int Validate(Address address, params Input[] inputs)
        {
            /* start counting errors
                * Response stores all the information about transaction,
                * also those that is positive 
                */
            int err = 0;

            /* the object is initiated in handler, need to refresh it with each transaction */
            this.Response = new Response<Address>();

            /* first check that the object is not null, 
                * will make it easier to catch other errors with transaction 
                */
            if (address == null)
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
                            if (this.ValidateIdIsNull(address))
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
        private bool ValidateIdIsNull(Address address)
        {
            return (address.ID == null || address.ID == 0);
        }
        private bool ValidateUserIDIsNull(Address address)
        {
            return (address.UserID == null || address.UserID == 0);
        }
        private bool ValidateUserAddressIsNull(Address address)
        {
            return (address.UserAddress == null || address.UserAddress == "");
        }
        #endregion
        #endregion

        public Response<Address> Update(Address address)
        {
            /* run validation
                * check that id and name are filled up)
                * */
            int err = this.Validate(address, Input.IdIsNull, Input.UserAddressIsNull, Input.UserIdIsNull);
            // if both fields are filled up, try to update the address 
            if (err < 1)
            {
                SqlData data = this.SetData(address); // translate c# to Sql
                this.Update(data, address.ID); // run transaction
                                                    /* the results of the transaction are stored 
                                                    * in this.Response including the SQL mesages and other custom notifications
                                                    * */

            }

            /* finish by adding the final address to response, 
                * (validation may correct some data without giving a visible to user error)
                * so we can use both the address and the messages later
                * */
            this.Response.Item = address;

            /*return all info we have about this transaction*/
            return this.Response;

        }
        public Response<Address> Delete(Address address)
        {
            /* relete method does not set a success on response 
                * (I dont remember why! Probably has something to do with batch and chain deletes)
                * rows affected can be zero However this.Success indicated there was no
                * exception thrown by SQL during transaction.
                * ToDo: Have a closer look at whats going on there
                * */

            int err = this.Validate(address, Input.IdIsNull);
            if (err < 1)
            {
                int rowsAffected = this.Delete(address.ID); // get rows deleted

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
        public Response<Address> Create(Address address)
        {
            //same procedure as update, just dont need id validation
            int err = this.Validate(address, Input.UserAddressIsNull, Input.UserIdIsNull);


            if (err < 1)
            {
                SqlData data = this.SetData(address);
                address.ID = this.InsertScopeId(data);
            }

            if (address.ID!=0) {
                this.Response.Success = true;
            }
            this.Response.Item = address;
            return this.Response;

        }

        public Address GetById(int id)
        {
            return this.GetAll().FirstOrDefault(x => x.ID == id);
        }

        internal Address GetByAddress(string address)
        {
            return this.GetAll().FirstOrDefault(x => x.UserAddress == address);
        }

        internal List<Address> GetByUserId(int userId) {
            return this.GetAll().Where(x => x.UserID == userId).ToList();
        }
    }
}
