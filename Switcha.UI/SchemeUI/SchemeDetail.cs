using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switcha.UI.Model;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.SchemeUI;

namespace Switcha.UI.SchemeUI
{
    public class SchemeDetail : EntityUI<Scheme>
    {
        public SchemeDetail()
        {
            AddSection()
                .WithTitle("Scheme Details")
                .IsFormGroup()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextLabel>().LabelTextIs("Name"),
                        Map(x => x.Route.Name).AsSectionField<TextLabel>().LabelTextIs("Route"),
                         Map(x => x.Combos).AsSectionField<Grid>().Of<Combo>()
                         .WithColumn(x => x.Combos, "Name")
                            .WithColumn(x => x.Fee.Name, "Fee")
                            .WithColumn(x=>x.TransactionType.Name, "Transaction Type")
                            .WithColumn(x=>x.Channel.Name, "Channel"),
                        Map(x => x.Description).AsSectionField<TextLabel>().LabelTextIs("Description"),
                        Map(x => x.dateCreated).AsSectionField<TextLabel>().LabelTextIs("Date Created"),
                        Map(x => x.dateUpdated).AsSectionField<TextLabel>().LabelTextIs("Last Modified")
                    })
                });

            AddButton()
                .WithText("Edit")
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.ApplicationEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<UpdateScheme>("Update")
                    .PrePopulate<Scheme, Scheme>(y => y));
        }
    }
}
