using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Accounting.Controllers
{
    public class ReportingController : Controller
    {
        public ActionResult Income()
        {
            return View();
        }

        public ActionResult Invoice()
        {
            return View();
        }
    }
}