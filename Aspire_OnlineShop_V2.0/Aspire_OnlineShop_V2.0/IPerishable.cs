using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    interface IPerishable
    {
        bool IsExpired();
        int TimeToExpire();
    }
}
