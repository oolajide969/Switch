using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.Model;

namespace Switcha.UI.SchemeUI
{
    public class UpdateScheme : EntityUI<Scheme>
    {
        public UpdateScheme()
        {
            AddSection()
             .WithTitle("Update Scheme")
             .IsFormGroup()
             .WithColumns(new List<Column>() {
                    new Column(new List<IField> ()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name"),

                        //Map(x => x.Route).AsSectionField<DropDownList>().Of(new SuperEntityLogic<Route>().GetAll()).ListOf(x => x.ID, x => x.Name).LabelTextIs("Route"),
                         Map(x => x.Route)
                      .AsSectionField<DropDownList>()
                      .Required()
                      .Of(new SuperEntityLogic<Route>().GetAll())
                      .ListOf(x => x.Name, x => x.ID),

                        Map(x => x.Combos)
                        .AsSectionField<DropDownList>()
                        .Of(new SuperEntityLogic<Combo>().GetAll())
                        .ListOf(x => x.Combos, x => x.ID).LabelTextIs("Combo"),


                        Map(x => x.Description).AsSectionField<TextArea>().LabelTextIs("Description"),

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
                        SuperEntityLogic<Scheme> SchemeLogic = new SuperEntityLogic<Scheme>();
                        SchemeLogic.Update(x);
                        SchemeLogic.Commit();
                        isSuccessful = true;
                        return isSuccessful;
                    }
                    catch (Exception)
                    {
                        isSuccessful = false;
                        return isSuccessful;
                        throw;
                    }
                }).OnSuccessDisplay("Scheme successfully Updated!!")
                  .OnFailureDisplay("An error occurred!");

        }
    }
}
