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
    public class UpdateCombo : EntityUI<ComboUIModel>
    {
        public UpdateCombo()
        {
            AddSection()
              .WithTitle("Update Combo")
              .IsFormGroup()
              .WithColumns(new List<Column>() {
                    new Column(new List<IField> ()
                    {
                        Map(x => x.Combos).AsSectionField<TextBox>().LabelTextIs("Name").TextFormatIs(TextFormat.name),
                        Map(x => x.TransactionType).AsSectionField<DropDownList>().Of(new SuperEntityLogic<TransactionType>().GetAll()).ListOf(x => x.Name, x => x.ID).LabelTextIs("Transaction Type"),
                        Map(x => x.Channel).AsSectionField<DropDownList>().Of(new SuperEntityLogic<Channels>().GetAll()).ListOf(x => x.Name, x => x.ID).LabelTextIs("Channel"),
                        Map(x => x.Fee).AsSectionField<DropDownList>().Of(new SuperEntityLogic<Fee>().GetAll()).ListOf(x => x.Name, x => x.ID).LabelTextIs("Fee"),

                    }),
              });

            AddButton()
                .WithText("Update")
                .ConfirmWith("Sure you want to update?")
                .SubmitTo(x =>
                {
                    bool isSuccessful = false;
                    try
                    {
                        SuperEntityLogic<Combo> ComboLogic = new SuperEntityLogic<Combo>();
                        ComboLogic.Update(x);
                        ComboLogic.Commit();
                        isSuccessful = true;
                        return isSuccessful;
                    }
                    catch (Exception)
                    {
                        isSuccessful = false;
                        return isSuccessful;
                        throw;
                    }
                }).OnSuccessDisplay("Combo successfully Updated")
                  .OnFailureDisplay("An error occurred!");

        }
    }
}
