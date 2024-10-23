using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;

namespace Switcha.Core.Mappings
{
    public class ComboMap : SuperEntityMap<Combo>
    {
        public ComboMap()
        {
            Map(x => x.Combos)
                .Not.Nullable();

            References(x => x.Channel)
                .Not.Nullable();

            References(x => x.Fee)
                .Not.Nullable();

            References(x => x.TransactionType)
                .Not.Nullable();
            
        }
    }
}
