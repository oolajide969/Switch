using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Switcha.Core.Models;

namespace Switcha.Core.Mappings
{
    public class SourceNodeMap : SuperEntityMap<SourceNode>
    {
        public SourceNodeMap()
        {
            Map(x => x.Name);
            Map(x => x.HostName);
            Map(x => x.IPAddress);
            Map(x => x.Port);
            Map(x => x.Status).CustomType<Status>();
            HasMany(x => x.Schemes).Not.LazyLoad();


        }
    }
}
