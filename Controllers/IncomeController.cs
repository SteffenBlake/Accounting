using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Accounting.Models;
using Accounting.Mappings;
using NHibernate;

namespace Accounting.Controllers
{
    public class IncomeController : Controller
    {
        public ActionResult Index()
        {
            using (ISession session = Hook.OpenSession())
            {
                var Incomes = session.QueryOver<VMIncome>().List();

                ViewBag.datasource = Incomes.AsEnumerable();
                ViewBag.Title = VMIncome.PluralName;
                return View();
            }
        }

        public ActionResult New()
        {
            using (ISession session = Hook.OpenSession())
            {
                ViewBag.TypeList = session.QueryOver<VMIncomeType>().List();
                ViewBag.AccountList = session.QueryOver<VMAccount>().List();

                ViewBag.Title = $"New {VMIncome.SingleName}";
                return View("Edit", new VMIncome());
            }
        }

        public ActionResult Edit(int id)
        {
            VMIncome vm;
            using (ISession session = Hook.OpenSession())
            {
                vm = session.Get<VMIncome>(id);

                vm.AccountId = vm.Account.Id;
                vm.IncomeTypeId = vm.IncomeType.Id;

                var TypeList = session.QueryOver<VMIncomeType>().List();
                var AccountList = session.QueryOver<VMAccount>().List();

                ViewBag.Title = $"Edit {VMIncome.SingleName}: {vm.Account.Name}-{vm.Date.ToString()}";
            }
            return View("Edit", vm);
        }

        [HttpPost]
        public ActionResult Edit(VMIncome vm)
        {
            using (ISession session = Hook.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        vm.SetType(session.Get<VMIncomeType>(vm.IncomeTypeId));
                        vm.SetAccount(session.Get<VMAccount>(vm.AccountId));

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

            using (ISession session = Hook.OpenSession())
            {
                try
                {
                    var vm = session.Get<VMIncome>(id);
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(vm);
                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}