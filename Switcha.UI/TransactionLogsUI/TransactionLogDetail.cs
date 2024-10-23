using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using Switcha.UI.Model;
using Switcha.Logic;
using Switcha.Core.Models;

namespace Switcha.UI.TransactionLogUI
{
    public class TransactionLogDetail : EntityUI<TransactionLogs>
    {
        public TransactionLogDetail()
        {
            AddSection()
                //.WithTitle("TransactionLog Details")
                .IsFormGroup()
                .WithColumns(new List<Column>()
                {
                    new Column(new List<IField>()
                    {

                        Map(x => x.MTI).AsSectionField<TextLabel>().LabelTextIs("MTI"),
                        Map(x => x.CardPAN).AsSectionField<TextLabel>().LabelTextIs("PAN"),
                        Map(x => x.STAN).AsSectionField<TextLabel>().LabelTextIs("STAN"),
                        Map(x => x.TransactionDate).AsSectionField<TextLabel>().LabelTextIs("Date"),
                        Map(x => x.Account1).AsSectionField<TextLabel>().LabelTextIs("Account1"),
                        Map(x => x.Account2).AsSectionField<TextLabel>().LabelTextIs("Account2"),
                        Map(x => x.Amount).AsSectionField<TextLabel>().LabelTextIs("Amount"),
                        Map(x => x.ResponseCode).AsSectionField<TextLabel>().LabelTextIs("Code"),
                        Map(x => x.TransactionDate).AsSectionField<TextLabel>().LabelTextIs("Transaction Date"),
                    })
                });

        }
    }
}

