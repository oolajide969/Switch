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

namespace Switcha.UI.TransactionTypeUI
{
    public class ViewTransactionType : EntityUI<TransactionTypeUIModel>
    {
        public ViewTransactionType()
        {
            WithTitle("Transaction Type Management");

            AddSection()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {

                        HasMany(x => x.TransactionTypes)
                            .AsSectionField<Grid>()
                            .Of<TransactionType>()
                            .WithColumn(x => x.Name)
                            .WithColumn(x => x.Code)
                            .WithColumn(x => x.Description)
                            .WithRowNumbers()
                            .IsPaged<TransactionTypeUIModel>(10, (x, pageDetails) =>
                            {
                               x.TransactionTypes = new SuperEntityLogic<TransactionType>().GetAll();
                               return x;
                            })
                            .ApplyMod<ViewDetailsMod>(y => y.Popup<TransactionTypeDetail>("TransactionType Details")
                                .PrePopulate<TransactionType, TransactionType>(z => z))
                   })
                });
        }
    }
}
