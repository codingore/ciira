using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ciira.Controllers
{
    public class SetupController : Controller
    {

        [Authorize]
        [Ciira.Business.CiiraAuthorize(Ciira.Models.UserKind.Administrator | Ciira.Models.UserKind.ProjectManager)]
        public ActionResult Index()
        {
            return View();
        }

    }
}
