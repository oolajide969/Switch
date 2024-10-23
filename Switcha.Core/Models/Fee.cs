using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switcha.Core.Models
{
    public class Fee : SuperEntity
    {
        public virtual string Name { get; set; }

        public virtual FeeEnum FeeOptions { get; set; }

        public virtual decimal Amount { get; set; }

        public virtual decimal Minimum { get; set; }

        public virtual decimal Maximum { get; set; }

    }
}
