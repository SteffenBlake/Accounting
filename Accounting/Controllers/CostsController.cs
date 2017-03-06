using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class CostsController : Controller
    {
        public ActionResult Index()
        {
            var Types = new List<VMInvoiceEntry>();

            using (ISession session = Hook.OpenSession())
            {
                Types.AddRange(session.QueryOver<VMInvoiceEntry>().List());

                ViewBag.datasource = Types.AsEnumerable();
                ViewBag.Title = VMInvoiceEntry.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            ViewBag.Title = $"New {VMInvoiceEntry.SingleName}";
            return View("Edit", new VMInvoiceEntry());
        }

        public ActionResult Edit(int id)
        {
            VMInvoiceEntry vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMInvoiceEntry>(id);
                ViewBag.Title = $"Edit {VMInvoiceEntry.SingleName}: {vm.Name}";
            }
            return View("Edit", vm);
        }

        [HttpPost]
        public ActionResult Edit(VMInvoiceEntry vm)
        {
            using (ISession session = Hook.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(vm);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return View();
                    }
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (ISession session = Hook.OpenSession())
                {
                    var vm = session.Get<VMInvoiceEntry>(id);
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(vm);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }
    }
}