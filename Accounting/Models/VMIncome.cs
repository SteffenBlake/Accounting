using System;

namespace Accounting.Models
{
    public class VMIncome : VMBase
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
        public virtual VMAccount Account
        {
            get { return _Account; }
            set { _Account = value; }
        }

        private VMIncomeType _IncomeType;
        public virtual VMIncomeType IncomeType
        {
            get { return _IncomeType; }
            set { _IncomeType = value; }
        }

        public override void DoSpecialLoad()
        {
            Date = DateTime.Now;
            Total = 0m;
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