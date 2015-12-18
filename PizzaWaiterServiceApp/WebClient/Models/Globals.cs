using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClient.PizzaWaiterTestServiceReference;

namespace WebClient.Models
{
    public static class Globals
    {
        public static List<Restaurant> Restaurants { get; set; }
        public static int UserInSessionID = 0; //This should actually be a session user id

    }
}