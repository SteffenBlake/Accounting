using System;

namespace Accounting.Models
{
    public class VMTransfer : VMBase
    {
        public static readonly string SingleName = "Transfer";
        public static readonly string PluralName = "Transfers";

        public virtual string LinkName => PluralName;

        private VMAccount _FromAccount;
        public virtual VMAccount FromAccount
        {
            get { return _FromAccount; }
            set { _FromAccount = value; }
        }

        private VMAccount _ToAccount;
        public virtual VMAccount ToAccount
        {
            get { return _ToAccount; }
            set { _ToAccount = value; }
        }

        private DateTime _Date;
        public virtual DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        private decimal _Amount;
        public virtual decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public override void DoSpecialLoad()
        {
            _Date = DateTime.Now;
            _Amount = 0m;
        }

        public virtual void SetAccounts(VMAccount fromAccount, VMAccount toAccount)
        {
            fromAccount.OutTransfers.Add(this);
            toAccount.InTransfers.Add(this);

            _FromAccount = FromAccount;
            _ToAccount = toAccount;
        }
    }
}