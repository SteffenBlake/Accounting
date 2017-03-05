using System;

namespace Accounting.Models
{
    public class VMIncome : VMBase
    {
        private DateTime _Date;
        private decimal _Total;
        private decimal? _FederalTax;
        private decimal? _ProvincialTax;
        private decimal? _CPP;
        private decimal? _EI;
        private VMAccount _Account;
        private VMIncomeType _IncomeType;

        public virtual DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public virtual decimal Total
        {
            get { return _Total; }
            set { _Total = value;}
        }

        public virtual decimal? FederalTax
        {
            get { return _FederalTax; }
            set { _FederalTax = value; }
        }

        public virtual decimal? ProvincialTax
        {
            get { return _ProvincialTax; }
            set { _ProvincialTax = value; }
        }

        public virtual decimal? CPP
        {
            get { return _CPP; }
            set { _CPP = value; }
        }

        public virtual decimal? EI
        {
            get { return _EI; }
            set { _EI = value; }
        }

        public virtual VMAccount Account
        {
            get { return _Account; }
            set { _Account = value; }
        }

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