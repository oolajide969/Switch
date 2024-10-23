using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switcha.Core.Models
{
    public class Channels : SuperEntity
    {
        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual string Description { get; set; }
    }
}
