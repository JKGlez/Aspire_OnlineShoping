using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OnlineShopV3.BO;
using OnlineShopV3.DL;

namespace OnlineShopV3.SL
{
    public class EmployeeController
    {
        InputValidation validInput = new InputValidation();
        EmployeeDataAccess employeeDL = new EmployeeDataAccess();

        public void HireEmployee(Employee theEmployee, List<Employee> ShopsEmployees)
        {
            Console.WriteLine(" -> Hire employee for the shop <-");
            Console.WriteLine("Insert the data of the new employee on one line:");
            Console.WriteLine("(productName,email,mobile,address,password,adminRights)");
            Console.WriteLine("example:");
            Console.WriteLine("Pedro,pedro@email.com,9876543210,street 10 number 10,pedro,true");

            Employee hiredEmployee = validInput.ValidatingNewEmployee();

            Console.WriteLine("New employee data:");
            Console.WriteLine(hiredEmployee);
            Console.WriteLine("continue?: (yes/no)");
            if (validInput.ValidatingYesNo().Equals("yes"))
            {
                Console.WriteLine("Insert admin password (" + theEmployee.Name + "): ");
                if (validInput.ValidatingPassword(theEmployee))
                {
                    Console.WriteLine("Password accepted, Employee hired...");
                    ShopsEmployees.Add(hiredEmployee);
                    employeeDL.WriteEmployeeDB(hiredEmployee);
                    //WriteEmployeeTXT(hiredEmployee.GetEmployeeString());
                }
                else
                {
                    Console.WriteLine("Invalid password, canceling action");
                }
            }
            else
            {
                Console.WriteLine("Canceling action...");
            }

        }

        public void FireEmployee(Employee theEmployee, List<Employee> ShopsEmployees)
        {

            Console.WriteLine("Select the number of the employee to fire:");
            Employee firedEmployee = validInput.ValidatingExitedEmployee(ShopsEmployees);
            Console.WriteLine("Employee to fire info::");
            Console.WriteLine("Name \tEmail \tMobile");
            Console.WriteLine(firedEmployee);
            Console.WriteLine("continue?: (yes/no)");
            if (validInput.ValidatingYesNo().Equals("yes"))
            {
                Console.WriteLine("Insert admin password (" + theEmployee.Name + "): ");
                if (validInput.ValidatingPassword(theEmployee))
                {
                    Console.WriteLine("Password accepted, Employee fired...");
                    ShopsEmployees.Remove(firedEmployee);
                    employeeDL.DeleteEmployeeDB(firedEmployee.Name);
                }
                else
                {
                    Console.WriteLine("Invalid password, canceling action");
                }
            }
            else
            {
                Console.WriteLine("Canceling action...");
            }

        }
      
    }
}
