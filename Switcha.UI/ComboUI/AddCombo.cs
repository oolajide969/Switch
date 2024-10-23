using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.Model;

namespace Switcha.UI.ComboUI
{
    public class AddCombo : EntityUI<ComboUIModel>
    {
        public AddCombo()
        {
            WithTitle("Combo"); //main page title

            Map(x => x.Combos).As<TextBox>()
            .TextFormatIs(TextFormat.name)
            .Required();

            Map(x => x.TransactionType).As<DropDownList>()
            .Of(new SuperEntityLogic<TransactionType>().GetAll())
            .ListOf(x => x.Name, x => x.ID)
            .LabelTextIs("Transaction Type")
            .Required();

            Map(x => x.Channel).As<DropDownList>()
                .Of(new SuperEntityLogic<Channels>().GetAll())
                .ListOf(x => x.Name, x => x.ID)
                .LabelTextIs("Channel")
                .Required();

            Map(x => x.Fee).As<DropDownList>()
                .Of(new SuperEntityLogic<Fee>().GetAll())
                .ListOf(x => x.Name, x => x.ID)
                .LabelTextIs("Fee")
                .Required();

                    AddButton()
                       .WithText("Create")
                       .SubmitTo(x =>
                       {
                           bool isSuccessful = false;
                           try
                           {
                               Combo combo = new Combo()
                               {
                                   Combos = x.Combos,
                                   TransactionType = x.TransactionType,
                                   Channel = x.Channel,
                                   Fee = x.Fee
                               };

                               SuperEntityLogic<Combo> ComboLogic = new SuperEntityLogic<Combo>();
                               ComboLogic.Insert(combo);
                               ComboLogic.Commit();
                               isSuccessful = true;
                               return isSuccessful;
                           }
                           catch (Exception)
                           {
                               isSuccessful = false;
                               return isSuccessful;
                           }
                       })
                       .OnSuccessDisplay("Combo saved successfully!!")
                       .OnFailureDisplay("An error occurred");
               
        }
    }
}
