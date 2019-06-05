using Artical_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Artical_Task.Controllers
{
    public class HomeController : Controller
    {

        //************************************************Login************************************************************/
        ArticalsDB db = new ArticalsDB();
        [HttpGet]

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Login(User user)
        {
            var myuser = db.User.SingleOrDefault(c => c.username.Equals(user.username) && c.password.Equals(user.password));
            if(myuser == null)
            {
                return Content("This user isn't exist...");
            }
            else
            {
                Session["UserID"] = myuser.id.ToString();
                Session["UserName"] = myuser.username.ToString();
                if (myuser.job_title == 0)
                {
                    return RedirectToAction("GetArts", "Admin");
                }
                else
                {
                    return RedirectToAction("GetArts", "Visitor");
                }
            }
            
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login", "Home");
        }
    }
}