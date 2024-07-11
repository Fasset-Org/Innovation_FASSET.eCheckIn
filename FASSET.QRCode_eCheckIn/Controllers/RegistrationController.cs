using FASSET.QRCode_eCheckIn.Data_Access;
using FASSET.QRCode_eCheckIn.Models;
using System;
using System.Web.Mvc;

namespace FASSET.QRCode_eCheckIn.Controllers
{
    public class RegistrationController : Controller
    {
        Data_Access.Dba _dbAccess = new Data_Access.Dba();

        public RegistrationController()
        {
            _dbAccess = new Data_Access.Dba();
        }

        // GET: Registration
        public ActionResult Index()
        {
            ViewBag.Departments = _dbAccess.GetDepartments();
            ViewBag.Employees = _dbAccess.GetEmployees();
            return View();
        }

        [HttpGet]
        public JsonResult GetDepartments(string term)
        {
            var departments = _dbAccess.GetDepartmentsByTerm(term);
            return Json(departments, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEmployees(string term)
        {
            var employees = _dbAccess.GetEmployeesByTerm(term);
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubmitRegistration(RegistrationMdl model)
        {
            model.qrCodeImgUrl = Request.Form["qrCodeImgUrl"];
            model.QRCodeTotp = Request.Form["QRCodeTotp"];
            model.userTotp = Request.Form["userTotp"];
            model.Employee = Request.Form["Employee"];
            model.GeoLocation = Request.Form["GeoLocation"];

            int res = _dbAccess.SaveRegistration(model);

            if (res == 1)
            {
                DateTime today = DateTime.Today;
                string dayOfWeek = today.DayOfWeek.ToString();
                string message = model.Employee + $" your Check-In is successful..Happy {dayOfWeek} !";
                TempData["CheckIn-Success"] = message;
                //ViewBag.Message = message;
                //ViewBag.MessageType = "success";
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.Message = "Registration failed!";
                ViewBag.MessageType = "error";
            }


            model.DepartmentList = _dbAccess.GetDepartments();
            model.EmployeeList = _dbAccess.GetEmployees();
            ViewBag.Departments = model.DepartmentList;
            ViewBag.Employees = model.EmployeeList;
            return View("Index", model);
        }
    }
}