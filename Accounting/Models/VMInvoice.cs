using System;
using System.Collections.Generic;

namespace Accounting.Models
{
    public class VMInvoice : VMBase
    {
        private List<VMInvoiceEntry> _Entries;
        public virtual List<VMInvoiceEntry> Entries
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

        public override void DoSpecialLoad()
        {
            _Entries = new List<VMInvoiceEntry>();

            _Date = DateTime.Now;
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