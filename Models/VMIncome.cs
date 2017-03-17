using System;
using System.Web.Script.Serialization;

namespace Accounting.Models
{
    public class VMIncome : VMTransactionBase
    {
        public static readonly string SingleName = "Income";
        public static readonly string PluralName = "Incomes";

        public virtual string LinkName => SingleName;

        private DateTime _Date;
        public virtual DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        private decimal _Total;
        public virtual decimal Total
        {
            get { return _Total; }
            set { _Total = value;}
        }

        private decimal? _FederalTax;
        public virtual decimal? FederalTax
        {
            get { return _FederalTax; }
            set { _FederalTax = value; }
        }

        private decimal? _ProvincialTax;
        public virtual decimal? ProvincialTax
        {
            get { return _ProvincialTax; }
            set { _ProvincialTax = value; }
        }

        private decimal? _CPP;
        public virtual decimal? CPP
        {
            get { return _CPP; }
            set { _CPP = value; }
        }

        private decimal? _EI;
        public virtual decimal? EI
        {
            get { return _EI; }
            set { _EI = value; }
        }

        private VMAccount _Account;
        [ScriptIgnore]
        public virtual VMAccount Account
        {
            get { return _Account; }
            set { _Account = value; }
        }

        private VMIncomeType _IncomeType;
        [ScriptIgnore]
        public virtual VMIncomeType IncomeType
        {
            get { return _IncomeType; }
            set { _IncomeType = value; }
        }
        public virtual int IncomeTypeId { get; set; }

        public virtual string AccountName => _Account != null ? _Account.Name : "";

        public virtual string OwnerName => _Account != null && _Account.Person != null ? _Account.Person.FullName : "";

        public virtual decimal Net => _Total - (_FederalTax ?? 0m) - (_ProvincialTax ?? 0m) - (_CPP ?? 0m) - (EI ?? 0m);

        public override void DoSpecialTransactionLoad()
        {
            Date = DateTime.Now;
            Total = 0m;
        }

        public override bool Validate()
        {
            return
                _Total > 0m &&
                _Account != null &&
                _IncomeType != null;
        }

        public virtual void SetType(VMIncomeType incomeType)
        {
            incomeType.Incomes.Add(this);

            IncomeType = incomeType;
        }

        public virtual void SetAccount(VMAccount account)
        {
            account.Incomes.Add(this);
            Account = account;
        }
    }
}