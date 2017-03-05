using System.Collections.Generic;

namespace Accounting.Models
{
    public class VMPerson : VMBase
    {
        private List<VMAccount> _Accounts;
        public virtual List<VMAccount> Accounts
        {
            get { return _Accounts; }
            set { _Accounts = value; }
        }

        private string _FirstName;
        public virtual string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _MiddleName;
        public virtual string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }

        private string _LastName;
        public virtual string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private int _Color;
        public virtual int Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        public override void DoSpecialLoad()
        {
            _Accounts = new List<VMAccount>();

            _FirstName = "";
            _MiddleName = "";
            _LastName = "";
            _Color = 0x000000; //Black
        }

        public virtual void AddAccount(VMAccount account)
        {
            account.Person = this;
            Accounts.Add(account);
        }
    }
}