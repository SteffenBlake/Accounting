using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class AmendmentsController : Controller
    {
        public ActionResult Index()
        {
            var Amendments = new List<VMAmendment>();

            using (ISession session = Hook.OpenSession())
            {
                Amendments.AddRange(session.QueryOver<VMAmendment>().List());

                ViewBag.datasource = Amendments.AsEnumerable();
                ViewBag.Title = VMAmendment.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            using (ISession session = Hook.OpenSession())
            {
                ViewBag.Title = $"New {VMAmendment.SingleName}";

                ViewBag.AccountList = session.QueryOver<VMAccount>().List();
                return View("Edit", new VMAmendment());
            }
        }

        public ActionResult Edit(int id)
        {
            using (ISession session = Hook.OpenSession())
            {
                var vm = session.Get<VMAmendment>(id);
                vm.AccountId = vm.Account.Id;

                ViewBag.AccountList = session.QueryOver<VMAccount>().List();

                ViewBag.Title = $"Edit {VMAmendment.SingleName}: {vm.AccountName}-{vm.Date.ToShortDateString()}";
                return View("Edit", vm);
            }
        }

        [HttpPost]
        public ActionResult Edit(VMAmendment vm)
        {
            if (vm.AccountId == null)
            {
                ViewBag.Error = "An Account must be selected.";
                return View("Edit", vm);
            }

            if (vm.Date < vm.Min)
            {
                ViewBag.Error = $"Invalid Date selected. Date must occur after {vm.Min.ToShortDateString()}";
                return View("Edit", vm);
            }
            using (ISession session = Hook.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        var Account = session.Get<VMAccount>(vm.AccountId);
                        if (Account == null)
                        {
                            ViewBag.Error = $"No such Account with Id {vm.AccountId} was found.";
                            return View("Edit", vm);
                        }

                        vm.SetAccount(Account);

                        session.SaveOrUpdate(vm);
                        transaction.Commit();

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();

                        ViewBag.Error = e.Message;
                        return View("Edit", vm);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (ISession session = Hook.OpenSession())
                {
                    var vm = session.Get<VMAmendment>(id);
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