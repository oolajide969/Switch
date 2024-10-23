using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switcha.Core.Models;
using Switcha.UI.Model;

namespace Switcha.UI.ChannelsUI
{
    public class ChannelsDetail : EntityUI<Channels>
    {
        public ChannelsDetail()
        {
            AddSection()
               .WithTitle("Channel Details")
               .IsFormGroup()
               .WithColumns(new List<Column>()
               {
                    new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextLabel>().LabelTextIs("Name"),
                        Map(x => x.Code).AsSectionField<TextLabel>().LabelTextIs("Code"),
                        Map(x => x.Description).AsSectionField<TextLabel>().LabelTextIs("Description"),
                        Map(x => x.dateCreated).AsSectionField<TextLabel>().LabelTextIs("Date Created"),
                        Map(x => x.dateUpdated).AsSectionField<TextLabel>().LabelTextIs("Last Modified")
                    })
               });

            AddButton()
                .WithText("Edit")
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.ApplicationEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<UpdateChannels>("Update")
                    .PrePopulate<Channels, Channels>(y => y));
        }
    }
}
