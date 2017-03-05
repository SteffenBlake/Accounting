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
        private List<VMIncome> _Incomes;
        private List<VMInvoice> _Invoices;
        private List<VMTransfer> _OutTransfers;
        private List<VMTransfer> _InTransfers;

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

        public virtual List<VMIncome> Incomes
        {
            get { return _Incomes; }
            set { _Incomes = value; }
        }

        public virtual List<VMInvoice> Invoices
        {
            get { return _Invoices; }
            set { _Invoices = value; }
        }

        public virtual List<VMTransfer> OutTransfers
        {
            get { return _OutTransfers; }
            set { _OutTransfers = value; }
        }

        public virtual List<VMTransfer> InTransfers
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