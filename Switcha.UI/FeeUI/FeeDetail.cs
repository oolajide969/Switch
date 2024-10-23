using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switcha.Core.Models;
using Switcha.UI.Model;

namespace Switcha.UI.FeeUI
{
    public class FeeDetail : EntityUI<Fee>
    {
        public FeeDetail()
        {
            AddSection()
               .WithTitle("Fee Details")
               .IsFormGroup()
               .WithColumns(new List<Column>()
               {
                    new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextLabel>().LabelTextIs("Name"),
                        Map(x => x.FeeOptions).AsSectionField<TextLabel>().LabelTextIs("Fee Option"),
                        Map(x => x.Amount).AsSectionField<TextLabel>().LabelTextIs("Amount"),
                        Map(x => x.Minimum).AsSectionField<TextLabel>().LabelTextIs("Minimum"),
                        Map(x => x.Maximum).AsSectionField<TextLabel>().LabelTextIs("Maximum"),
                        Map(x => x.dateCreated).AsSectionField<TextLabel>().LabelTextIs("Date Created"),
                        Map(x => x.dateUpdated).AsSectionField<TextLabel>().LabelTextIs("Last Updated")
                    })
               });

            AddButton()
                .WithText("Edit")
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.ApplicationEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<UpdateFee>("Update")
                    .PrePopulate<Fee, Fee>(y => y));
        }
    }
}
