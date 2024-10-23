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
    public class ViewRoute : EntityUI<RouteUIModel>
    {
        public ViewRoute()
        {
            WithTitle("Route Management");
            AddSection()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {

                        HasMany(x => x.RouteList)
                            .AsSectionField<Grid>()
                            .Of<Route>()
                            .WithColumn(x => x.Name)
                            .WithColumn(x => x.sinkNode.Name, "Sink Node")
                            .WithColumn(x => x.CardPAN)
                            .WithColumn(x => x.Description)
                            .WithRowNumbers()
                            .IsPaged<RouteUIModel>(10, (x, pageDetails) =>
                            {
                               x.RouteList = new SuperEntityLogic<Route>().GetAll();
                               return x;
                            })
                            .ApplyMod<ViewDetailsMod>(y => y.Popup<RouteDetail>("Route Details")
                                .PrePopulate<Route, Route>(z => z))
                   })
                });
        }
    }
}
