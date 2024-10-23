using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.Model;
using Switcha.UI.TransactionLogUI;

namespace Switcha.UI.TransactionLogsUI
{
    public class ViewTransactionLog : EntityUI<TransactionLogsUIModel>
    {
        public ViewTransactionLog()
        {
            AddSection()
                .WithTitle("Transaction Logs")
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {

                        HasMany(x => x.TransactionLogList)
                            .AsSectionField<Grid>()
                            .Of<TransactionLogs>()
                            .WithColumn(x => x.CardPAN)
                            .WithColumn(x => x.Amount)
                            .WithRowNumbers()
                            .IsPaged<TransactionLogsUIModel>(10, (x, pageDetails) =>
                            {
                               x.TransactionLogList = new SuperEntityLogic<TransactionLogs>().GetAll();
                               return x;
                            })
                            .ApplyMod<ViewDetailsMod>(y => y.Popup<TransactionLogDetail>("Transaction Log Details")
                                .PrePopulate<TransactionLogs, TransactionLogs>(z => z))
                   })
                });
        }
    }
}
