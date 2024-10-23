using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switcha.Core.Models
{
    public class Combo : SuperEntity
    {
        public virtual string Combos { get; set; }

        public virtual TransactionType TransactionType { get; set; }

        public virtual Channels Channel { get; set; }

        public virtual Fee Fee { get; set; }

    }
}
