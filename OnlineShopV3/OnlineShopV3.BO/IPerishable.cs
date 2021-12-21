using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopV3.BO
{
    interface IPerishable
    {
        bool IsExpired();
        int TimeToExpire();
    }
}
