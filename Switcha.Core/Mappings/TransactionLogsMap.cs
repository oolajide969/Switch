using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using Switcha.Core.Models;

namespace Switcha.Core.Mappings
{
    public class TransactionLogsMap : SuperEntityMap<TransactionLogs>
    {
        public TransactionLogsMap()
        {
            Map(x => x.MTI);

            Map(x => x.CardPAN);

            Map(x => x.Amount)
                .Not.Nullable();

            Map(x => x.STAN);

            Map(x => x.ResponseCode);

            Map(x => x.Account1);

            Map(x => x.Account2);

            Map(x => x.TransactionDate);

        }
    }
}
