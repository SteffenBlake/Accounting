using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accounting.Models
{
    public class VMBase    {

        private int _Id;
        public virtual int Id
        {
            get { return _Id; }
            protected set { _Id = value; }
        }

        public bool IsNew => Id == 0;

        public VMBase()
        {
            _Id = 0;
            DoSpecialLoad();
        }

        public virtual void DoSpecialLoad() { }
    }
}