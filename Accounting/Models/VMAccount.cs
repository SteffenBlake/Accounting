using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accounting.Models
{
    public class VMAccount : VMBase
    {

        private string _Name;
        private VMPerson _Person;
        private VMAccountType _AccountType;
        private IList<VMIncome> _Incomes;
        private IList<VMInvoice> _Invoices;
        private IList<VMTransfer> _OutTransfers;
        private IList<VMTransfer> _InTransfers;

        public virtual string Name
        {
            get { return _Name; }
            set {_Name = value; }
        }

        public virtual VMPerson Person
        {
            get { return _Person; }
            set { _Person = value; }
        }

        public virtual VMAccountType AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }

        public virtual IList<VMIncome> Incomes
        {
            get { return _Incomes; }
            set { _Incomes = value; }
        }

        public virtual IList<VMInvoice> Invoices
        {
            get { return _Invoices; }
            set { _Invoices = value; }
        }

        public virtual IList<VMTransfer> OutTransfers
        {
            get { return _OutTransfers; }
            set { _OutTransfers = value; }
        }

        public virtual IList<VMTransfer> InTransfers
        {
            get { return _InTransfers; }
            set { _InTransfers = value; }
        }

        public override void DoSpecialLoad() 
        {
            _Name = "";
            _OutTransfers = new List<VMTransfer>();
            _InTransfers = new List<VMTransfer>();
            _Incomes = new List<VMIncome>();
            _Invoices = new List<VMInvoice>();
        }

        public virtual void SetType(VMAccountType accountType)
        {
            accountType.Accounts.Add(this);
            AccountType = accountType;
        }
    }
}