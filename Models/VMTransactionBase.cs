using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accounting.Models
{
    public class VMTransactionBase : VMBase    {

        private bool _IsPosted;
        public virtual bool IsPosted {
            get
            {
                return _IsPosted;
            }
            set
            {
                _IsPosted = value;
            }
        }

        public override void DoSpecialLoad()
        {
            _IsPosted = false;
            DoSpecialTransactionLoad();
        }

        public virtual void DoSpecialTransactionLoad() { }

        public virtual bool IsValid => Validate();

        public virtual bool Validate() { return false; }

    }
}