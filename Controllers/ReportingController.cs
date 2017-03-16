using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

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