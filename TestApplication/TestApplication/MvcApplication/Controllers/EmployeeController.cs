using System.Linq;
using System.Web.Mvc;
using MvcApplication.Models;

namespace MvcApplication.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult GetEmployees()
        {
            return View("Index", EmployeeStore.FetchEmployees());
        }

        public ActionResult GetEmployee(int id)
        {
            var emp = EmployeeStore.EmployeeList.FirstOrDefault(x => x.EmpId == id);
            return View("EmployeeDetail", emp);
        }

        [HttpPost]
        public ActionResult SetEmployee(Employee emp)
        {
            var employee = EmployeeStore.EmployeeList.FirstOrDefault(x => x.EmpId == emp.EmpId);
            if (employee == null)
            {
                return View("EmployeeDetail", emp);
            }
            employee.Name = emp.Name;
            employee.Salary = emp.Salary;

            return View("EmployeeDetail", emp);
        }

        public ActionResult AddEmployee()
        {
            return View("AddEmployee");
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee emp)
        {
            EmployeeStore.EmployeeList.Add(new Employee
            {
                EmpId = EmployeeStore.EmployeeList.Count == 0 ? 1 : EmployeeStore.EmployeeList.Max(x => x.EmpId) + 1,
                Name = emp.Name,
                Salary = emp.Salary
            });
            return View("EmployeeDetail", emp);
        }

        [HttpPost]
        public void DeleteEmployee(int[] empIds)
        {
            EmployeeStore.EmployeeList.RemoveAll(x => empIds.Contains(x.EmpId));
        }
    }
}