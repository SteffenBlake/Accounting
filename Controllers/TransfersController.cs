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
            var Transfers = new List<VMTransfer>();

            using (ISession session = Hook.OpenSession())
            {
                Transfers.AddRange(session.QueryOver<VMTransfer>().List());

                ViewBag.datasource = Transfers.AsEnumerable();
                ViewBag.Title = VMTransfer.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            using (ISession session = Hook.OpenSession())
            {
                ViewBag.Title = $"New {VMTransfer.SingleName}";
                var Accounts = session.QueryOver<VMAccount>().List();
                ViewBag.AccountList = Accounts.AsEnumerable();

                if (Accounts.Count == 0)
                    ViewBag.Error = "Warning: No Accounts detected. Please create some!";

                return View("Edit", new VMTransfer());
            }
        }

        public ActionResult Edit(int id)
        {
            VMTransfer vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMTransfer>(id);
                vm.FromAccountId = vm.FromAccount.Id;
                vm.ToAccountId = vm.ToAccount.Id;

                ViewBag.Title = $"Edit {VMTransfer.SingleName}: {vm.FromAccount.Name} → {vm.ToAccount.Name}";

                ViewBag.AccountList = session.QueryOver<VMAccount>().List().AsEnumerable();
                return View("Edit", vm);
            }
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
                ViewBag.Error = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}