using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Accounting.Models
{
    public class VMAccount : VMBase
    {
        public static readonly string SingleName = "Account";
        public static readonly string PluralName = "Accounts";

        public virtual string LinkName => PluralName;

        private string _Name;
        public virtual string Name
        {
            get { return _Name; }
            set {_Name = value; }
        }

        private decimal _OpeningBalance;
        public virtual decimal OpeningBalance
        {
            get { return _OpeningBalance; }
            set { _OpeningBalance = value; }
        }

        private DateTime _CreationDate;
        public virtual DateTime CreationDate
        {
            get { return _CreationDate; }
            set { _CreationDate = value; }
        }

        private VMPerson _Person;
        [ScriptIgnore]
        public virtual VMPerson Person
        {
            get { return _Person; }
            set { _Person = value; }
        }
        public virtual int? OwnerId { get; set; }

        private VMAccountType _AccountType;
        [ScriptIgnore]
        public virtual VMAccountType AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }

        private IList<VMIncome> _Incomes;
        public virtual IList<VMIncome> Incomes
        {
            get { return _Incomes; }
            set { _Incomes = value; }
        }

        private IList<VMInvoice> _Invoices;
        public virtual IList<VMInvoice> Invoices
        {
            get { return _Invoices; }
            set { _Invoices = value; }
        }

        private IList<VMTransfer> _OutTransfers;
        public virtual IList<VMTransfer> OutTransfers
        {
            get { return _OutTransfers; }
            set { _OutTransfers = value; }
        }

        private IList<VMTransfer> _InTransfers;
        public virtual IList<VMTransfer> InTransfers
        {
            get { return _InTransfers; }
            set { _InTransfers = value; }
        }

        public virtual string Owner => _Person != null ? _Person.FullName : "";
        public virtual string FullName => _Person != null ? $"{_Person.FullName} - {Name}" : "";
        public virtual string Type => _AccountType != null ? _AccountType.Name : "";
        public virtual int? TypeId { get; set; }

        public virtual decimal Balance => 0m;

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