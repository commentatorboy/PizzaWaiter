using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaWaiterServiceLibrary.Handlers {
    public static class Tools {

        public static T StringToEnum<T>(this string value) {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
