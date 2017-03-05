using System.Collections.Generic;

namespace Accounting.Models
{
    public class VMTypeBase : VMBase
    {
        private string _Name;
        public virtual string Name        {
            get { return _Name; }
            set { _Name = value; }
        }

        public override void DoSpecialLoad()
        {
            _Name = "";
            DoSpecialTypeLoad();
        }

        public virtual void DoSpecialTypeLoad() {}
    }

    public class VMAccountType : VMTypeBase {
        private List<VMAccount> _Accounts;
        public List<VMAccount> Accounts {
            get
            {
                return _Accounts;
            }
            set
            {
                _Accounts = value;
            }
        }

        public override void DoSpecialTypeLoad()
        {
            _Accounts = new List<VMAccount>();
        }
    }

    public class VMPlaceType : VMTypeBase {
        private List<VMInvoice> _Invoices;
        public List<VMInvoice> Invoices
        {
            get
            {
                return _Invoices;
            }
            set
            {
                _Invoices = value;
            }
        }

        public override void DoSpecialTypeLoad()
        {
            _Invoices = new List<VMInvoice>();
        }
    }

    public class VMCostType : VMTypeBase {
        private List<VMInvoiceEntry> _Entries;
        public List<VMInvoiceEntry> Entries
        {
            get
            {
                return _Entries;
            }
            set
            {
                _Entries = value;
            }
        }

        public override void DoSpecialTypeLoad()
        {
            _Entries = new List<VMInvoiceEntry>();
        }
    }

    public class VMIncomeType : VMTypeBase {
        private List<VMIncome> _Incomes;
        public List<VMIncome> Incomes
        {
            get
            {
                return _Incomes;
            }
            set
            {
                _Incomes = value;
            }
        }

        public override void DoSpecialTypeLoad()
        {
            _Incomes = new List<VMIncome>();
        }
    }

    public class VMTaxType : VMTypeBase
    {

        private decimal _Rate;
        public virtual decimal Rate        {
            get { return _Rate; }
            set { _Rate = value; }
        }

        private List<VMInvoice> _Invoices;
        public List<VMInvoice> Invoices
        {
            get
            {
                return _Invoices;
            }
            set
            {
                _Invoices = value;
            }
        }

        public override void DoSpecialTypeLoad()
        {
            _Invoices = new List<VMInvoice>();
            _Rate = 0m;
        }
    }
}