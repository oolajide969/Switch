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

namespace Switcha.UI.ComboUI
{
    public class ViewCombo : EntityUI<ComboUIModel>
    {
        public ViewCombo()
        {
            WithTitle("Combo Management");

            AddSection()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {

                        HasMany(x => x.ComboList)
                            .AsSectionField<Grid>()
                            .Of<Combo>()
                            .WithColumn(x => x.Combos)
                            .WithColumn(x => x.TransactionType.Name, "Transaction Type")
                            .WithColumn(x => x.Channel.Name, "Channel")
                            .WithColumn(x => x.Fee.Name, "Fee")
                            .WithRowNumbers()
                            .IsPaged<ComboUIModel>(10, (x, pageDetails) =>
                            {
                               x.ComboList = new SuperEntityLogic<Combo>().GetAll();
                               return x;
                            })
                            .ApplyMod<ViewDetailsMod>(y => y.Popup<ComboDetail>("Combo Details")
                                .PrePopulate<Combo, Combo>(z => z))
                   })
                });
        }
    }
}
