using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.Model;

namespace Switcha.UI.FeeUI
{
    public class UpdateFee : EntityUI<Fee>
    {
        public UpdateFee()
        {
            AddSection()
              .WithTitle("Update Fee")
              .IsFormGroup()
              .WithColumns(new List<Column>() {
                    new Column(new List<IField> ()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name").TextFormatIs(TextFormat.name),
                        Map(x => x.FeeOptions).AsSectionField<DropDownList>().Of<OptionAndValue>(() =>{return FeeUIModel.Options;}).ListOf(x => x.Option, x => x.Value).LabelTextIs("Fee Option"),
                        Map(x => x.Amount).AsSectionField<TextBox>().LabelTextIs("Amount").TextFormatIs(TextFormat.numeric),
                        Map(x => x.Minimum).AsSectionField<TextBox>().LabelTextIs("Minimum").TextFormatIs(TextFormat.numeric),
                        Map(x => x.Maximum).AsSectionField<TextBox>().LabelTextIs("Minimum").TextFormatIs(TextFormat.numeric),


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
                        SuperEntityLogic<Fee> FeeLogic = new SuperEntityLogic<Fee>();
                        FeeLogic.Update(x);
                        FeeLogic.Commit();
                        isSuccessful = true;
                        return isSuccessful;
                    }
                    catch (Exception)
                    {
                        isSuccessful = false;
                        return isSuccessful;
                        throw;
                    }
                }).OnSuccessDisplay("Fee successfully Updated")
                  .OnFailureDisplay("An error occurred!");
        }
    }
}
