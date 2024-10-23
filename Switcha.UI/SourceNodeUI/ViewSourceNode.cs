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
    public class ViewSourceNode : EntityUI<SourceNodeUIModel>
    {
        public ViewSourceNode()
        {
            WithTitle("Source Node Management");
            AddSection()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {

                        HasMany(x => x.SourceNodeList)
                            .AsSectionField<Grid>()
                            .Of<SourceNode>()
                            .WithColumn(x => x.Name)
                            .WithColumn(x => x.HostName)
                            .WithColumn(x => x.IPAddress)
                            .WithColumn(x => x.Port)
                            .WithColumn(x => x.Status)
                            .WithRowNumbers()
                            .IsPaged<SourceNodeUIModel>(10, (x, pageDetails) =>
                            {
                               x.SourceNodeList = new SuperEntityLogic<SourceNode>().GetAll();
                               return x;
                            })
                            .ApplyMod<ViewDetailsMod>(y => y.Popup<SourceNodeDetail>("Source Node Details")
                                .PrePopulate<SourceNode, SourceNode>(z => z))
                     })
              });
        }
    }
}
