using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    class ExceptionNumberOnRange : Exception
    {
        public ExceptionNumberOnRange() 
        { 
        
        }

        public ExceptionNumberOnRange(string message)
            :base(String.Format("Invalid number: {0}",message)) 
        { 
        
        }

        public int ValidatingNumberRange(int bN, int tN)
        {
            try
            {
                int tryInt = int.Parse(Console.ReadLine());

                if (tryInt >= bN && tryInt <= tN)
                {
                    return tryInt;
                }
                else
                {
                    Console.WriteLine("Number out of range, try again:");
                    return ValidatingNumberRange(bN, tN);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingNumberRange(bN,tN);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingNumberRange(bN, tN);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingNumberRange(bN, tN);
            }
        }
    }
}

