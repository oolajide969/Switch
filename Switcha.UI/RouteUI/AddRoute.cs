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
    public class AddRoute : EntityUI<RouteUIModel>
    {
        public AddRoute()
        {
            WithTitle("Route"); //main page title

            Map(x => x.Name).As<TextBox>()
                .WithLength(50)
                .LabelTextIs("Route name") //a custom label
                .Required()         //a required field
                .TextFormatIs(@"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$");  //regex for name. You can validate any textbox using the same procedure

            Map(x => x.sinkNode).As<DropDownList>()
                .Of(new SuperEntityLogic<SinkNode>().GetAll())
                        .ListOf(x => x.Name, x => x.ID)
                .LabelTextIs("Sink Node"); //a custom label

            Map(x => x.CardPAN).As<TextBox>()
                .TextFormatIs(TextFormat.numeric)
                .WithLength(19)                
                .LabelTextIs("Card PAN") //a custom label
                .Required();

            Map(x => x.Description).As<TextArea>()
                .Required();

            AddButton()
                .WithText("Create")
                .SubmitTo(x =>
                {
                    bool isSuccessful = false;
                    try
                    {
                        Route route = new Route()
                        {
                            Name = x.Name,
                            sinkNode = x.sinkNode,
                            CardPAN = x.CardPAN,
                            Description = x.Description
                        };

                        SuperEntityLogic<Route> RouteLogic = new SuperEntityLogic<Route>();
                        RouteLogic.Insert(route);
                        RouteLogic.Commit();
                        isSuccessful = true;
                        return isSuccessful;
                    }
                    catch (Exception)
                    {
                        isSuccessful = false;
                        return isSuccessful;
                    }
                })
                .OnSuccessDisplay("Route saved successfully!!")
                .OnFailureDisplay("An error occurred");
        }
    }
}
