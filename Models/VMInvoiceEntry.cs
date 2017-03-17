namespace Accounting.Models
{
    public class VMInvoiceEntry : VMTransactionBase
    {
        public static readonly string SingleName = "Entry";
        public static readonly string PluralName = "Entries";

        public virtual string LinkName => "Costs";

        private VMInvoice _Invoice;
        public virtual VMInvoice Invoice
        {
            get { return _Invoice; }
            set { _Invoice = value; }
        }

        private VMCostType _CostType;
        public virtual VMCostType CostType
        {
            get { return _CostType; }
            set { _CostType = value; }
        }

        private string _Name;
        public virtual string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private int _Quantity;
        public virtual int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private decimal _Amount;
        public virtual decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private decimal? _Discount;
        public virtual decimal? Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        public override void DoSpecialLoad()
        {
            _Name = "";
            _Quantity = 1;
            _Amount = 0m;
        }

        public virtual void SetInvoice(VMInvoice invoice)
        {
            invoice.Entries.Add(this);

            Invoice = invoice;
        }

        public virtual void SetCostType(VMCostType costType)
        {
            costType.Entries.Add(this);
            CostType = costType;
        }
    }
}