using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Switcha.Core.Models;

namespace Switcha.Core.Mappings
{
    public class SinkNodeMap : SuperEntityMap<SinkNode>
    {
        public SinkNodeMap()
        {
            Map(x => x.Name)
                .Not.Nullable()
                .Length(50);

            Map(x => x.HostName)
                .Not.Nullable()
                .Length(70);

            Map(x => x.IPAddress)
                .Not.Nullable();

            Map(x => x.Port)
                .Not.Nullable();

            Map(x => x.Status).CustomType<Status>();

        }
    }
}
