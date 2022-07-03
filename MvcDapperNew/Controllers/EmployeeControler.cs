using Microsoft.AspNetCore.Mvc;
using MvcDapperNew.Models;
using Dapper;

namespace MvcDapperNew.Controllers
{
    public class EmployeeControler : Controller
    {
        public IActionResult Index()
        {
            return View(DapperORM.ReturnList<EmployeeModel>("EmployeeViewAll"));
        }
        [HttpGet]
        public IActionResult AddOrEdit(int id =0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("EmployeeId", id);
                return View(DapperORM.ReturnList<EmployeeModel>("EmployeeViewById",param).FirstOrDefault<EmployeeModel>());
            }
        }
        [HttpPost]
        public IActionResult AddOrEdit(EmployeeModel emp)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeId",emp.EmployeeID);
            param.Add("@Name",emp.Name);
            param.Add("@Position",emp.Position);
            param.Add("@Age",emp.Age);
            param.Add("@Salary",emp.Salary);
            DapperORM.ExecuteWithourReturn("EmployeeAddOrEdit",param);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("EmployeeId", id);
            DapperORM.ExecuteWithourReturn("EmployeeDeleteById", param);
            return RedirectToAction("Index");
        }
    }
}
