using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switcha.Core.Models
{
    public class TransactionLogs : SuperEntity
    {
        public virtual string CardPAN { get; set; }

        public virtual string MTI { get; set; } 

        public virtual decimal Amount { get; set; }

        public virtual string STAN { get; set; }

        public virtual DateTime TransactionDate { get; set; }

        public virtual string TypeOfEntry { get; set; }

        public virtual string Account1 { get; set; }

        public virtual string Account2 { get; set; }

        public virtual string ResponseCode { get; set; }
    }
}
