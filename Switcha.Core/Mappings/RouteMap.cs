using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Switcha.Core.Models;

namespace Switcha.Core.Mappings
{
    public class RouteMap : SuperEntityMap<Route>
    {
        public RouteMap()
        {
            Map(x => x.Name)
                .Not.Nullable();

            Map(x => x.CardPAN)
                .Not.Nullable();

            Map(x => x.Description)
                .Not.Nullable();

            References(x => x.sinkNode)
                .Not.Nullable();
        }
    }
}
