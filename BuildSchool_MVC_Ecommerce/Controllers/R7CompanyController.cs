using BuildSchool.MvcSolution.OnlineStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BuildSchool_MVC_Ecommerce.Controllers
{
    public class R7CompanyController : Controller
    {
        // GET: R7Company
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string employeeID, string phone)
        {
            var cookieName = "Employee";
            var employeesRepository = new EmployeesRepository();
            var id = 0;
            int.TryParse(employeeID, out id);
            var employee = employeesRepository.FindById(id);
            if(employee!= null)
            {
                if(id == employee.EmployeeID && phone == employee.Phone)
                {
                    if (Response.Cookies.AllKeys.Contains(cookieName))
                    {
                        return RedirectToAction("Home", "R7Company");
                    }
                    //var token = Guid.NewGuid().ToString();
                    //HttpContext.Application[token] = DateTime.UtcNow.AddHours(12);
                    var hc = new HttpCookie(cookieName, HttpUtility.UrlEncode(employee.EmployeeID.ToString()))
                    {
                        Expires = DateTime.Now.AddMinutes(30),
                        HttpOnly = true
                    };
                    Response.Cookies.Add(hc);
                    return RedirectToAction("Home", "R7Company");
                }
            }
            return RedirectToAction("Index", "R7Company");
            //if (member != null)
            //{
            //    if (memberid == member.MemberID && password == member.Password)
            //    {
            //        if (Response.Cookies.AllKeys.Contains(cookieName))
            //        {
            //            var cookieVal = Response.Cookies[cookieName].Value;
            //            HttpContext.Application.Remove(cookieVal);
            //            Response.Cookies.Remove(cookieName);
            //        }
            //        var token = Guid.NewGuid().ToString();
            //        //HttpContext.Application[token] = DateTime.UtcNow.AddHours(12);
            //        var user = new User()
            //        {
            //            UserID = member.MemberID,
            //            Username = member.Name
            //        };
            //        string json = JSONSerializer.Serialize(user);
            //        var hc = new HttpCookie(cookieName, HttpUtility.UrlEncode(json))
            //        {
            //            Expires = DateTime.Now.AddMinutes(20),
            //            HttpOnly = true
            //        };
            //        Response.Cookies.Add(hc);
            //    }
            //}
            //return RedirectToAction("Index", "Home");
        }
    }
}