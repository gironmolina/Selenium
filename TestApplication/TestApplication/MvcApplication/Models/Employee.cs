using System.Collections.Generic;

namespace MvcApplication.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
    }

    public class EmployeeStore
    {
        public static List<Employee> FetchEmployees()
        {
            if (EmployeeList != null)
            {
                return EmployeeList;
            }

            var empList = new List<Employee>
            {
                new Employee {EmpId = 1, Name = "Anderson", Salary = 2000},
                new Employee {EmpId = 2, Name = "Johnson", Salary = 3000},
                new Employee {EmpId = 3, Name = "Jackson", Salary = 4000}
            };
            EmployeeList = empList;
            return EmployeeList;
        }

        public static List<Employee> EmployeeList;
    }
}