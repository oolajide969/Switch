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

namespace Switcha.UI.ChannelsUI
{
    public class ViewChannels : EntityUI<ChannelsUIModel>
    {
        public ViewChannels()
        {
            WithTitle("Channels Management");

            AddSection()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {

                        HasMany(x => x.ChannelsList)
                            .AsSectionField<Grid>()
                            .Of<Channels>()
                            .WithColumn(x => x.Name)
                            .WithColumn(x => x.Code)
                            .WithColumn(x => x.Description)
                            .WithRowNumbers()
                            .IsPaged<ChannelsUIModel>(10, (x, pageDetails) =>
                            {
                               x.ChannelsList = new SuperEntityLogic<Channels>().GetAll();
                               return x;
                            })
                            .ApplyMod<ViewDetailsMod>(y => y.Popup<ChannelsDetail>("Channels Details")
                                .PrePopulate<Channels, Channels>(z => z))
                   })
                });
        }
    }
}
