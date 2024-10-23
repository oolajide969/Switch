using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switcha.UI.Model;
using Switcha.Core.Models;
using Switcha.Logic;

namespace Switcha.UI.SchemeUI
{
    public class ViewScheme : EntityUI<SchemeUIModel>
    {
        public ViewScheme()
        {
            WithTitle("Scheme Management"); 

            AddSection()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {

                        HasMany(x => x.SchemeList)
                            .AsSectionField<Grid>()
                            .Of<Scheme>()
                            .WithColumn(x => x.Name)
                            .WithColumn(x => x.Description)
                            .WithRowNumbers()
                            .IsPaged<SchemeUIModel>(10, (x, pageDetails) =>
                            {
                               x.SchemeList = new SuperEntityLogic<Scheme>().GetAll();
                               return x;
                            })
                            .ApplyMod<ViewDetailsMod>(y => y.Popup<SchemeDetail>("Scheme Details")
                                .PrePopulate<Scheme, Scheme>(z => z))
                   })
                });
        }
    }
}
