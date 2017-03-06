using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class TransfersController : Controller
    {
        public ActionResult Index()
        {
            var Types = new List<VMTransfer>();

            using (ISession session = Hook.OpenSession())
            {
                Types.AddRange(session.QueryOver<VMTransfer>().List());

                ViewBag.datasource = Types.AsEnumerable();
                ViewBag.Title = VMTransfer.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            ViewBag.Title = $"New {VMTransfer.SingleName}";
            return View("Edit", new VMTransfer());
        }

        public ActionResult Edit(int id)
        {
            VMTransfer vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMTransfer>(id);
                ViewBag.Title = $"Edit {VMTransfer.SingleName}: {vm.FromAccount.Name} → {vm.ToAccount.Name}";
            }
            return View("Edit", vm);
        }

        [HttpPost]
        public ActionResult Edit(VMTransfer vm)
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
                    var vm = session.Get<VMTransfer>(id);
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