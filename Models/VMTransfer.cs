using System;
using System.Web.Script.Serialization;

namespace Accounting.Models
{
    public class VMTransfer : VMTransactionBase
    {
        public static readonly string SingleName = "Transfer";
        public static readonly string PluralName = "Transfers";

        public virtual string LinkName => PluralName;

        private VMAccount _FromAccount;
        [ScriptIgnore]
        public virtual VMAccount FromAccount
        {
            get { return _FromAccount; }
            set { _FromAccount = value; }
        }
        public virtual int FromAccountId { get; set; }

        private VMAccount _ToAccount;
        [ScriptIgnore]
        public virtual VMAccount ToAccount
        {
            get { return _ToAccount; }
            set { _ToAccount = value; }
        }
        public virtual int ToAccountId { get; set; }

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

        public override void DoSpecialTransactionLoad()
        {
            _Date = DateTime.Now;
            _Amount = 0m;
        }

        public override bool Validate()
        {
            return 
                _Amount > 0m &&
                _FromAccount != null &&
                _ToAccount != null;
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