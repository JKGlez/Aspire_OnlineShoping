using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnlineShopV3.BO;

namespace OnlineShopV3.SL
{
    public class InputValidation : Exception
    {
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
                Console.WriteLine("General exception ocurred:\n"+ex.Message+"\ntry again:");
                return ValidatingProdcutByID(theProducts);
            }
        }
        public int ValidatingID(List<Product> theProducts)
        {
            try
            {
                int selectedID = int.Parse(Console.ReadLine());

                IEnumerable<Product> productD = from product in theProducts
                                                where product.Id == selectedID
                                                select product;
                if (productD.Count() != 0)
                    return productD.First().Id;
                else
                {
                    Console.WriteLine("ID not found, try again:");
                    return ValidatingID(theProducts);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("The ID can't have Letters (FormatException), try again:");
                return ValidatingID(theProducts);
            }
            catch (OverflowException)
            {
                Console.WriteLine("The ID can't be so BIG (OverflowException), try again:");
                return ValidatingID(theProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General exception ocurred:\n" + ex.Message + "\ntry again:");
                return ValidatingID(theProducts);
            }
        }
        //CHECK VALIDATION OF DATA
        public Employee ValidatingNewEmployee()
        {
            try
            {
                string tryEmpoyee = Console.ReadLine();
                string[] tryEmpoyeeData = tryEmpoyee.Split(',');

                string name = tryEmpoyeeData[0];
                string email = tryEmpoyeeData[1];
                long mobile = long.Parse(tryEmpoyeeData[2]);
                string addres = tryEmpoyeeData[3];
                string password = tryEmpoyeeData[4];
                bool adminR = tryEmpoyeeData[5].ToLower().Equals("true");

                Employee newEmployee= new Employee(name, email, mobile, addres, password, adminR);
                
                return newEmployee;

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingNewEmployee();
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingNewEmployee();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingNewEmployee();
            }
        }
        public Employee ValidatingExitedEmployee(List<Employee> theEmployees)
        {
            try
            {
                int tryE = ValidatingNumberRange(0, theEmployees.Count);

                return theEmployees[tryE];

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingNewEmployee();
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingNewEmployee();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingNewEmployee();
            }
        }
        public bool ValidatingPassword(Employee theEmployee)
        {
            try
            {
                string inputString = (Console.ReadLine());

                if (inputString.Equals(theEmployee.Password))
                {
                    return true;
                }

                else
                {
                    int count = 1;
                    do
                    {
                        Console.WriteLine("Invalid Password, attempt ("+count+"/3)");
                        inputString = (Console.ReadLine());

                        if (inputString.Equals(theEmployee.Password))
                        {
                            return true;
                        }
                        else
                        {
                            count++;
                        }
                    } while (count < 3);
                    Console.WriteLine("Access DENIED.");
                    return false;
                }

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingPassword(theEmployee);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingPassword(theEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingPassword(theEmployee);
            }
        }
        public string ValidatingYesNo()
        {
            try
            {
                string inputString = (Console.ReadLine());

                if (inputString.ToLower().Equals("yes"))
                {
                    return "yes";
                }
                else if (inputString.ToLower().Equals("no"))
                {
                    return "no";
                }
                else
                {
                    Console.WriteLine("Invalid answer, try again:");
                    return ValidatingYesNo();
                }
                    
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingYesNo();
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingYesNo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ValidatingYesNo();
            }
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

