using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Switcha.Core.Models;
 
namespace Switcha.Core.Mappings
{
    public class SchemeMap : SuperEntityMap<Scheme>
    {
        public SchemeMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);
            References(x => x.Route);
            HasMany(x => x.Combos).Not.LazyLoad();
        }
    }
}
