using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Switcha.Core.Models;

namespace Switcha.Core.Mappings
{
    public class FeeMap : SuperEntityMap<Fee>
    {
        public FeeMap() 
        {
            Map(x => x.Name)
                .Length(50)
                .Not.Nullable();

            Map(x => x.FeeOptions)
                .Not.Nullable();

            Map(x => x.Amount)
                .Not.Nullable();

            Map(x => x.Minimum)
                .Not.Nullable();

            Map(x => x.Maximum)
                .Not.Nullable();
        }
    }
}
