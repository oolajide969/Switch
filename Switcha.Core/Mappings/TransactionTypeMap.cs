using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Switcha.Core.Models;  

namespace Switcha.Core.Mappings
{
    public class TransactionTypeMap : SuperEntityMap<TransactionType>
    {
        public TransactionTypeMap()
        {
            Map(x => x.Name)
                .Length(50)
                .Not.Nullable();

            Map(x => x.Code)
                .Not.Nullable();

            Map(x => x.Description)
                .Not.Nullable();
        }
    }
}
