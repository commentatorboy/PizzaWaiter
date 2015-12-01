using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    enum OrderStatus{
        Default = 0,
        WAITING = 1,
        COOKING = 2,
        TRANPORTING = 3,
        RECIEVED = 4
    }
}
