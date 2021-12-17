using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    public class Program
    {

        public static void Main(string[] args)
        {
            InputValidation validInput = new InputValidation();
            

            //int answerMenu;
            //int answerLogin;
            Shop theShop = new Shop();
            theShop.ReadProductsDB();
            //theShop.RegisterProductPD();
            //theShop.RegisterEmployeesPD();
            ////theShop.RegisterCostumersPD();

            //Menu:
            //do
            //{
            //    Console.WriteLine("Select the profile to enter the online shop:");
            //    Console.WriteLine("1.- Employee");
            //    Console.WriteLine("2.- Costumer");
            //    Console.WriteLine("3.- Exit program");
            //    try
            //    {
            //        answerLogin = validInput.ValidatingNumberRange(0, 3);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        answerLogin = 0;
            //    }
            //    switch (answerLogin)
            //    {
            //        case 1:
            //            Employee userE;
            //            Console.Clear();
            //            try
            //            {
            //                userE = theShop.LoginE();
            //                theShop.EmployeesMenu(userE);
            //                goto Menu;
            //            }
            //            catch(NullReferenceException ex)
            //            {
            //                Console.WriteLine(ex.Message);
            //                Console.WriteLine("Inavalid access, closing the program");
            //                Environment.Exit(0);
            //            }
            //            break;

            //        case 2:
            //            Costumer userC;
            //            Console.Clear();
            //            try
            //            {
            //                userC = theShop.LoginC();
            //                theShop.CostumerMenu(userC);
            //                goto Menu;
            //            }
            //            catch (NullReferenceException ex)
            //            {
            //                Console.WriteLine(ex.Message);
            //                Console.WriteLine("Inavalid access, closing the program");
            //                Environment.Exit(0);
            //            }
            //            break;

            //        case 4:
            //            Console.Clear();
            //            Console.WriteLine(" -> Exing program <-");
            //            Console.WriteLine("Press any key exit...");
            //            Console.ReadKey();
            //            Environment.Exit(0);
            //            break;
            //    }
            //    //theShop.RegisterProduct();
            //    //theShop.PrintRegisteredProducts();
            //    //Console.WriteLine("Continue? (1/0)");
            //    answerMenu = int.Parse(Console.ReadLine());
            //} while (answerMenu != 0);


        }
    }
}
