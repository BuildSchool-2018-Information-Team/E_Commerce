using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuildSchool.MvcSolution.OnlineStore.Models;
using BuildSchool.MvcSolution.OnlineStore.Repository;

namespace BuildSchool_MVC_Ecommerce.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignUp(string memberid, string password, string password1, string name, string email, string phone, string address)
        {
            var member = new Members()
            {
                MemberID = memberid,
                Password = password,
                Name = name,
                Email = email,
                Address = address,
                Phone = phone
            };
            var memberrepository = new MemberRepository();
            var members = memberrepository.FindById(member.MemberID);
            if(members == null)
            {
                memberrepository.Create(member);
            }
            else
            {
                return View("SignUpError");
            }
            return View();
        }
        public ActionResult SignUpError()
        {
            return View();
        }
    }
}