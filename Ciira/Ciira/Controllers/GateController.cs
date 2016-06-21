using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciira.Controllers
{
    public class GateController : Controller
    {

        public ActionResult Index()
        {
            Ciira.Models.LoginModel model = new Models.LoginModel();
            model.Email = "";
            model.Password = "";
            model.ReturnUrl = "";

            return View(model);
        }

        public ActionResult Logout()
        {
            new Ciira.Business.GateBusiness().Logout();
            return RedirectToAction("Index", "Gate");
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            Ciira.Models.LoginModel model = new Models.LoginModel();
            model.Email = "";
            model.Password = "";
            model.ReturnUrl = "";
            if (returnUrl != null)
            {
                model.ReturnUrl.Trim();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(Ciira.Models.LoginModel model)
        {
            string message = "";

            message = new Ciira.Business.GateBusiness().Login(model);

            if (message.Length == 0)
            {
                if (model.ReturnUrl != null && model.ReturnUrl.Trim().Length > 0)
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Gate");
                }
            }

            ViewBag.Message = message;

            return View(model);
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult ServerError()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

    }
}
