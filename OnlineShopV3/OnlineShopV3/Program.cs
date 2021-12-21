using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnlineShopV3.SL;

namespace OnlineShopV3.PL
{
    class Program
    {
        static void Main(string[] args)
        {
            ShopController theShop = new ShopController();
            theShop.ShopMenu();
        }
    }
}
