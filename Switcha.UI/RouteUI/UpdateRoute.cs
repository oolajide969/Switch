using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.Model;

namespace Switcha.UI.RouteUI
{
    public class UpdateRoute : EntityUI<Route>
    {
        public UpdateRoute()
        {
            AddSection()
              .WithTitle("Update Route")
              .IsFormGroup()
              .WithColumns(new List<Column>() {
                    new Column(new List<IField> ()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name").TextFormatIs(TextFormat.name),
                        Map(x => x.sinkNode).AsSectionField<DropDownList>().Of(new SuperEntityLogic<SinkNode>().GetAll()).ListOf(x => x.Name, x => x.ID).LabelTextIs("Sink Node"),
                        Map(x => x.CardPAN).AsSectionField<TextBox>().LabelTextIs("Card PAN"),
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
                        SuperEntityLogic<Route> RouteLogic = new SuperEntityLogic<Route>();
                        RouteLogic.Update(x);
                        RouteLogic.Commit();
                        isSuccessful = true;
                        return isSuccessful;
                    }
                    catch (Exception)
                    {
                        isSuccessful = false;
                        return isSuccessful;
                        throw;
                    }
                }).OnSuccessDisplay("Route successfully Updated")
                  .OnFailureDisplay("An error occurred!");

        }
    }
}
