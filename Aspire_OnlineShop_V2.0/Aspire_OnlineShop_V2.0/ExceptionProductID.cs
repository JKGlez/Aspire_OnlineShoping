using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    class ExceptionProductID : Exception
    {
        public ExceptionProductID() 
        { 
        
        }

        public ExceptionProductID(string message)
            :base(String.Format("Invalid ID for a product: {0}",message)) 
        { 
        
        }

        public Product ValidatingProdcutByID(List<Product> theProducts)
        {
            try
            {
                int selectedID = int.Parse(Console.ReadLine());

                IEnumerable<Product> productD = from product in theProducts
                                                where product.Id == selectedID
                                                select product;
                if (productD.Count() != 0)
                    return productD.First();
                else
                {
                    Console.WriteLine("ID not found, try again:");
                    return ValidatingProdcutByID(theProducts);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("The ID can't have Letters (FormatException), try again:");
                return ValidatingProdcutByID(theProducts);
            }
            catch (OverflowException)
            {
                Console.WriteLine("The ID can't be so BIG (OverflowException), try again:");
                return ValidatingProdcutByID(theProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General exception ocurred:\n" + ex.Message + "\ntry again:");
                return ValidatingProdcutByID(theProducts);
            }
        }

    }
}

