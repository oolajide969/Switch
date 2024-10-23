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

namespace Switcha.UI.TransactionTypeUI
{
    public class TransactionTypeDetail : EntityUI<TransactionType>
    {
        public TransactionTypeDetail()
        {
            AddSection()
                .WithTitle("Transaction Type Details")
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
                .ApplyMod<ButtonPopupMod>(x => x.Popup<UpdateTransactionType>("Update")
                    .PrePopulate<TransactionType, TransactionType>(y => y));
        }
    }
}

