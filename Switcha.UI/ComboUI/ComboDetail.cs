using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switcha.Core.Models;
using Switcha.UI.Model;

namespace Switcha.UI.ComboUI
{
    public class ComboDetail : EntityUI<ComboUIModel>
    {
        public ComboDetail()
        { 
            AddSection()
                .WithTitle("Combo Details")
                .IsFormGroup()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {
                        Map(x => x.TransactionType.Name).AsSectionField<TextLabel>().LabelTextIs("Transaction Type"),
                        Map(x => x.Channel.Name).AsSectionField<TextLabel>().LabelTextIs("Channel"),
                        Map(x => x.Fee.Name).AsSectionField<TextLabel>().LabelTextIs("Fee"),
                        Map(x => x.dateCreated).AsSectionField<TextLabel>().LabelTextIs("Date Created"),
                        Map(x => x.dateUpdated).AsSectionField<TextLabel>().LabelTextIs("Last Modified")
                    })
                });

            AddButton()
                .WithText("Edit")
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.ApplicationEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<UpdateCombo>("Update")
                    .PrePopulate<Combo, Combo>(y => y));

        }
    }
}
