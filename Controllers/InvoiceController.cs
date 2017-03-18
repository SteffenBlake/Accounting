using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class InvoiceController : Controller
    {
        public ActionResult Index()
        {
            var Types = new List<VMInvoice>();

            using (ISession session = Hook.OpenSession())
            {
                Types.AddRange(session.QueryOver<VMInvoice>().List());

                ViewBag.datasource = Types.AsEnumerable();
                ViewBag.Title = VMInvoice.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            ViewBag.Title = $"New {VMInvoice.SingleName}";
            return View("Edit", new VMInvoice());
        }

        public ActionResult Edit(int id)
        {
            VMInvoice vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMInvoice>(id);
                ViewBag.Title = $"Edit {VMInvoice.SingleName}:{vm.Account.Name}-{vm.Date.ToString()}";
            }
            return View("Edit", vm);
        }

        [HttpPost]
        public ActionResult Edit(VMInvoice vm)
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
                        ViewBag.Error = e.Message;
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
                    var vm = session.Get<VMInvoice>(id);
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
                ViewBag.Error = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}