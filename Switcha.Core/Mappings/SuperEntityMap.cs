using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Switcha.Core.Models;

namespace Switcha.Core.Mappings
{
    public class SuperEntityMap<T> : ClassMap<T> where T : SuperEntity
    {
        public SuperEntityMap()
        {
            Id(x => x.ID);
            Map(x => x.dateCreated);
            Map(x => x.dateUpdated);
        }
        
    }
}
