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

namespace Switcha.UI.FeeUI
{
    public class ViewFee : EntityUI<FeeUIModel>
    {
        public ViewFee()
        {
            WithTitle("Fee Management");

            AddSection()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {

                        HasMany(x => x.Fees)
                            .AsSectionField<Grid>()
                            .Of<Fee>()
                            .WithColumn(x => x.Name)
                            .WithColumn(x => x.FeeOptions)
                            .WithColumn(x => x.Amount)
                            .WithColumn(x => x.Minimum)
                            .WithColumn(x => x.Maximum)
                            .WithRowNumbers()
                            .IsPaged<FeeUIModel>(10, (x, pageDetails) =>
                            {
                               x.Fees = new SuperEntityLogic<Fee>().GetAll();
                               return x;
                            })
                            .ApplyMod<ViewDetailsMod>(y => y.Popup<FeeDetail>("Fee Details")
                                .PrePopulate<Fee, Fee>(z => z))
                   })
                });
        }
    }
}
