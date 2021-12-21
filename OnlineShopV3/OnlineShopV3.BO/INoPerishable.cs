using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopV3.BO
{
    interface INoPerishable
    {
        DateTime GuaranteedUntil();
        void ExtendsGuarantee();
    }
}
