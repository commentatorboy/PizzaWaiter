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
    public class Favorite : SqlModel
    {

        private FavoriteDB favoriteDB; //related DB handler (required)
        private DishDB dishDB;
        private UserDB userDB;

        /*Object properties (custom)*/
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int DishID { get; set; }
        [DataMember]
        public Dish Dish { get; set; }
        [DataMember]
        public User User { get; set; }

        /*Build Object (required)*/
        public void BuildObject(DataRow row)
        {
            this.ID = SqlFormat.ToInt(row, "ID");
            this.UserID = SqlFormat.ToInt(row, "UserID");
            this.DishID = SqlFormat.ToInt(row, "DishID");

            this.Connect();
            this.Dish = this.dishDB.GetById(this.DishID);
            this.User = this.userDB.GetById(UserID);
            
        }

        /* Connects to handler, only once per object
         * (required for the cases when object is passed from client)
         */
        public void Connect()
        {
            if (this.favoriteDB == null)
            {
                this.favoriteDB = new FavoriteDB();
            }
            if(this.userDB == null)
            {
                this.userDB = new UserDB();
            }
            if(this.dishDB == null)
            {
                this.dishDB = new DishDB();
            }
        }

        /* Adds favorite to db */
        public Response<Favorite> Create()
        {
            this.Connect();
            return this.favoriteDB.Create(this);
        }

        /* Updates favorite in database */
        public Response<Favorite> Update()
        {
            this.Connect();
            return this.favoriteDB.Update(this);
        }

        /*Deletes favorite from database
         * add here manual cascades, if are needed
         */
        public Response<Favorite> Delete()
        {
            this.Connect();
            return this.favoriteDB.Delete(this);
        }

    }

    /* Communicates object to database */
    public class FavoriteDB : SqlHandler<Favorite>
    {

        /* translate data from c# to sql */
        private SqlData SetData(Favorite i)
        {
            SqlData data = new SqlData();
            //tell translator to which db rows which data belongs
            //data.Set("ID", i.ID);
            data.Set("UserID", i.UserID);
            data.Set("DishID", i.DishID);
            return data;
        }

        #region Validation

        /* possible validations */
        enum Input
        {
            IdIsNull,
            UserIDIsNull,
            DishIDIsNull
        }

        /* Validate data stored in the object */
        private int Validate(Favorite favorite, params Input[] inputs)
        {
            /* start counting errors
             * Response stores all the information about transaction,
             * also those that is positive 
             */
            int err = 0;

            /* the object is initiated in handler, need to refresh it with each transaction */
            this.Response = new Response<Favorite>();

            /* first check that the object is not null, 
             * will make it easier to catch other errors with transaction 
             */
            if (favorite == null)
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
                            if (this.ValidateIdIsNull(favorite))
                            {
                                this.Response.AddMessage(ResponseMessage.DataEmpty); // add message
                                err++; // count errors up
                            }
                            break;
                            /*
                        case Input.TitleIsNull:
                            if (this.ValidateTitleIsNull(favorite)) {
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
        private bool ValidateIdIsNull(Favorite favorite)
        {
            return (favorite.ID == null || favorite.ID == 0);
        }
        /*
        private bool ValidateTitleIsNull(Favorite favorite) {
            return (favorite.Title == null || favorite.Title == "");
        }*/
        #endregion
        #endregion

        public Response<Favorite> Update(Favorite favorite)
        {
            /* run validation
             * check that id and name are filled up)
             * */
            int err = this.Validate(favorite, Input.IdIsNull, Input.UserIDIsNull, Input.DishIDIsNull);
            // if both fields are filled up, try to update the favorite 
            if (err < 1)
            {
                SqlData data = this.SetData(favorite); // translate c# to Sql
                this.Update(data, favorite.ID); // run transaction
                /* the results of the transaction are stored 
                 * in this.Response including the SQL mesages and other custom notifications
                 * */

            }

            /* finish by adding the final favorite to response, 
             * (validation may correct some data without giving a visible to favorite error)
             * so we can use both the favorite and the messages later
             * */
            this.Response.Item = favorite;

            /*return all info we have about this transaction*/
            return this.Response;

        }
        public Response<Favorite> Delete(Favorite favorite)
        {
            /* relete method does not set a success on response 
             * (I dont remember why! Probably has something to do with batch and chain deletes)
             * rows affected can be zero However this.Success indicated there was no
             * exception thrown by SQL during transaction.
             * ToDo: Have a closer look at whats going on there
             * */

            int err = this.Validate(favorite, Input.IdIsNull);
            if (err < 1)
            {
                int rowsAffected = this.Delete(favorite.ID); // get rows deleted

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
        public Response<Favorite> Create(Favorite favorite)
        {
            //same procedure as update, just dont need id validation
            int err = this.Validate(favorite, Input.UserIDIsNull, Input.DishIDIsNull);


            if (err < 1)
            {
                SqlData data = this.SetData(favorite);
                favorite.ID = this.InsertScopeId(data);
            }

            this.Response.Item = favorite;
            return this.Response;

        }

        public Favorite GetById(int id)
        {
            return this.GetAll().FirstOrDefault(x => x.ID == id);
        }

        internal List<Favorite> GetByUserID(int userid)
        {
            return this.GetAll().Where(x => x.UserID == userid).ToList();
        }

        internal bool Exists(int userID, int dishID)
        {
            Favorite favorite = this.GetAll().FirstOrDefault(x => x.DishID == dishID && x.UserID == userID);
            return (favorite != null);
        }
    }
}
