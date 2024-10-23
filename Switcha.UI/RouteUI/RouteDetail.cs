using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;
using Switcha.Logic;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switcha.UI.Model;

namespace Switcha.UI.RouteUI
{
    public class RouteDetail : EntityUI<Route>
    {
        public RouteDetail()
        {
            AddSection()
                //.WithTitle("Route Details")
                .IsFormGroup()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextLabel>().LabelTextIs("Name"),
                        Map(x => x.sinkNode.Name).AsSectionField<TextLabel>().LabelTextIs("Sink Node"),
                        Map(x => x.CardPAN).AsSectionField<TextLabel>().LabelTextIs("Card PAN"),
                        Map(x => x.Description).AsSectionField<TextLabel>().LabelTextIs("Description"),
                        Map(x => x.dateCreated).AsSectionField<TextLabel>().LabelTextIs("Date Created"),
                        Map(x => x.dateUpdated).AsSectionField<TextLabel>().LabelTextIs("Last Modified")
                    })
                });

            AddButton()
                .WithText("Edit")
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.ApplicationEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<UpdateRoute>("Update")
                    .PrePopulate<Route, Route>(y => y));
        }
    }
}
