using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ciira.Business
{

    public class GateBusiness
    {

        public Ciira.Models.User CurrentUser()
        {
            if (System.Web.HttpContext.Current.Session["CurrentUser"] == null)
            {
                return null;
            }
            try
            {
                Ciira.Models.User model = (Ciira.Models.User)System.Web.HttpContext.Current.Session["CurrentUser"];
                return model;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void Logout()
        {
            System.Web.HttpContext.Current.Session["CurrentUser"] = null;
        }

        public string Login(Ciira.Models.LoginModel model)
        {
            string message = "";

            if (message.Length == 0)
            {
                if (model.Email == null || model.Email.Trim().Length == 0)
                {
                    message = "Email is required!";
                }
            }

            if (message.Length == 0)
            {
                if (model.Password == null || model.Password.Length == 0)
                {
                    message = "Password is required!";
                }
            }

            if (message.Length == 0)
            {
                using (Ciira.Models.CiiraEntities db = new Models.CiiraEntities()) 
                {
                    Ciira.Models.User u = db.Users.Where(x => x.Email.ToLower() == model.Email.Trim().ToLower()).FirstOrDefault();
                    if (u == null)
                    {
                        message = "User is not found!";
                    }
                    else
                    {
                        if (!u.Password.Equals(ToolBusiness.EncryptPassword(model.Password)))
                        {
                            message = "Password does not match!";
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Session["CurrentUser"] = u;
                            System.Web.Security.FormsAuthentication.SetAuthCookie(u.Code, false);
                        }
                    }
                }
            }

            return message;
        }

    }

}