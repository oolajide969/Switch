using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switcha.Core.Models;
using Switcha.UI.Model;

namespace Switcha.UI.SinkNodeUI
{
    public class SinkNodeDetail : EntityUI<SinkNode>
    {
        public SinkNodeDetail()
        {
            AddSection()
              .WithTitle("Sink Node Details")
              .IsFormGroup()
              .WithColumns(new List<Column>()
              {
                    new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextLabel>().LabelTextIs("Name"),
                        Map(x => x.HostName).AsSectionField<TextLabel>().LabelTextIs("Host Name"),
                        Map(x => x.IPAddress).AsSectionField<TextLabel>().LabelTextIs("IP Address"),
                        Map(x => x.Port).AsSectionField<TextLabel>().LabelTextIs("Port"),
                        Map(x => x.dateCreated).AsSectionField<TextLabel>().LabelTextIs("Date Created"),
                        Map(x => x.dateUpdated).AsSectionField<TextLabel>().LabelTextIs("Last Updated")
                    })
              });

            AddButton()
                .WithText("Edit")
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.ApplicationEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<UpdateSinkNode>("Update")
                    .PrePopulate<SinkNode, SinkNode>(y => y));
        }
    }
}
