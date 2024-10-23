using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switcha.Core.Models
{
    public class SourceNode : SuperEntity
    {
        public virtual string Name { get; set; }

        public virtual string HostName { get; set; }

        public virtual string IPAddress { get; set; }

        public virtual int Port { get; set; }

        public virtual IList<Scheme> Schemes { get; set; }

        public virtual Status Status { get; set; }
    }
}
