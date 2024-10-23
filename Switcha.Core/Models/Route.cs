using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switcha.Core.Models
{
    public class Route : SuperEntity
    {
        public virtual string Name { get; set; }

        public virtual SinkNode sinkNode { get; set; }

        public virtual string CardPAN { get; set; }

        public virtual string Description { get; set; }
    } 
}
