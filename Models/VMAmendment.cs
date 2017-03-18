using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Accounting.Models
{
    public class VMAmendment : VMTransactionBase
    {
        public static readonly string SingleName = "Amendment";
        public static readonly string PluralName = "Amendments";

        public virtual string LinkName => PluralName;

        private decimal _Amount;
        [DisplayName("New Amount")]
        public virtual decimal NewAmount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        private DateTime _Date;
        public virtual DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        private VMAccount _Account;
        [ScriptIgnore]
        public virtual VMAccount Account
        {
            get { return _Account; }
            set { _Account = value; }
        }
        public virtual int? AccountId { get; set; }
        public virtual DateTime Min => Account.CreationDate;
        public virtual string AccountName => Account.Name;
        

        public override void DoSpecialTransactionLoad()
        {
            _Date = DateTime.Now;
        }

        public override bool Validate()
        {

            return
                 Account != null &&
                 Date >= Min;
        }

        public virtual void SetAccount(VMAccount account)
        {
            account.Amendments.Add(this);
            Account = account;
        }

    }
}