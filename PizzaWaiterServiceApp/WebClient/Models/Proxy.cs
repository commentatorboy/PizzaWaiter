using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClient.PizzaWaiterTestServiceReference;

namespace WebClient.Models {
    public class Proxy {

        private static IPizzaWaiterTestService proxy;
        public static IPizzaWaiterTestService Get() {
            if (proxy==null) {
                proxy = new PizzaWaiterTestServiceClient();
            }
            return proxy;
        }

    }
}