using System;
using System.Collections.Generic;
using System.Linq;

namespace Accounting.Models
{
    public class VMInvoice : VMTransactionBase
    {
        public static readonly string SingleName = "Invoice";
        public static readonly string PluralName = "Invoices";

        public virtual string LinkName => PluralName;

        private IList<VMInvoiceEntry> _Entries;
        public virtual IList<VMInvoiceEntry> Entries
        {
            get { return _Entries; }
            set { _Entries = value; }
        }

        private VMTaxType _TaxType;
        public virtual VMTaxType TaxType
        {
            get { return _TaxType; }
            set { _TaxType = value; }
        }

        private VMPlaceType _PlaceType;
        public virtual VMPlaceType PlaceType
        {
            get { return _PlaceType; }
            set { _PlaceType = value; }
        }

        private VMAccount _Account;
        public virtual VMAccount Account
        {
            get { return _Account; }
            set { _Account = value; }
        }

        private DateTime _Date;
        public virtual DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public override void DoSpecialTransactionLoad()
        {
            _Entries = new List<VMInvoiceEntry>();

            _Date = DateTime.Now;
        }

        public override bool Validate()
        {

            return
                _Entries.Count > 0 &&
                 Entries.All(e => e.IsValid) &&
                 _TaxType != null &&
                 _PlaceType != null &&
                 _Account != null;
        }

        public virtual void SetAccount(VMAccount account)
        {
            account.Invoices.Add(this);

            Account = account;
        }

        public virtual void SetTaxType(VMTaxType taxType)
        {
            taxType.Invoices.Add(this);
            TaxType = taxType;
        }

        public virtual void SetPlaceType(VMPlaceType placeType)
        {
            placeType.Invoices.Add(this);
            PlaceType = placeType;
        }
    }
}