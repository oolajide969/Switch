using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Logic;
using Switcha.Core.Models;
using Switcha.UI.Model;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;

namespace Switcha.UI.SourceNodeUI
{
    public class SourceNodeDetail : EntityUI<SourceNode>
    {
        public SourceNodeDetail()
        {
            AddSection()
                .IsFormGroup()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextLabel>().LabelTextIs("Name"),
                        Map(x => x.HostName).AsSectionField<TextLabel>().LabelTextIs("Host Name"),
                        Map(x => x.IPAddress).AsSectionField<TextLabel>().LabelTextIs("IPAddress"),
                        Map(x => x.Port).AsSectionField<TextLabel>().LabelTextIs("Port"),
                        Map(x => x.Status).AsSectionField<TextLabel>().LabelTextIs("Status"),
                        Map(x => x.dateCreated).AsSectionField<TextLabel>().LabelTextIs("Date Created"),
                        Map(x => x.dateUpdated).AsSectionField<TextLabel>().LabelTextIs("Last Modified"),
                        Map(x => x.Schemes).AsSectionField<Grid>().Of<Scheme>()
                            .WithColumn(x => x.Name, "List of Schemes for this Source")
                    })
                });

            AddButton()
                .WithText("Edit")
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.ApplicationEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<UpdateSourceNode>("Edit")
                    .PrePopulate<SourceNode, SourceNode>(y => y));
        }
    }
}
