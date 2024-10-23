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

namespace Switcha.UI.SinkNodeUI
{
    public class ViewSinkNode : EntityUI<SinkNodeUIModel>
    {
        public ViewSinkNode()
        {
            WithTitle("Sink Node Management");

            AddSection()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {

                        HasMany(x => x.SInkNodeList)
                            .AsSectionField<Grid>()
                            .Of<SinkNode>()
                            .WithColumn(x => x.Name)
                            .WithColumn(x => x.HostName)
                            .WithColumn(x => x.IPAddress)
                            .WithColumn(x => x.Port)
                            .WithColumn(x => x.Status)
                            .WithRowNumbers()
                            .IsPaged<SinkNodeUIModel>(10, (x, pageDetails) =>
                            {
                               x.SInkNodeList = new SuperEntityLogic<SinkNode>().GetAll();
                               return x;
                            })
                            .ApplyMod<ViewDetailsMod>(y => y.Popup<SinkNodeDetail>("Sink Node Details")
                                .PrePopulate<SinkNode, SinkNode>(z => z))
                   })
                });
        }
    }
}
