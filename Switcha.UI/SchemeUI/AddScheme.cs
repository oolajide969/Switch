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
    public class AddScheme : EntityUI<SchemeUIModel>
    {
        public AddScheme()
        {
            WithTitle("Scheme"); //main page title

            Map(x => x.Name).As<TextBox>()
            .TextFormatIs(@"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$") //regex for name. You can validate any textbox using the same procedure
            .LabelTextIs("Scheme Name")
            .Required();

            Map(x => x.Route).As<DropDownList>()
            .Of(new SuperEntityLogic<Route>().GetAll())
            .ListOf(x => x.Name, x => x.ID)
            .LabelTextIs("Route"); //a custom label

            Map(x => x.Combos).As<DropDownList>()
                .Of(new SuperEntityLogic<Combo>().GetAll())
                .ListOf(x => x.Combos, x => x.ID)
                .LabelTextIs("Combo Name");
            
            Map(x => x.Description).As<TextArea>()
                    .Required();

            AddButton()
                .WithText("Create")
                .SubmitTo(x =>
                {
                    bool isSuccessful = false;
                        try
                        {
                            Scheme scheme = new Scheme()
                            {
                                Name = x.Name,                                    
                                Route = x.Route,
                                Combos = x.Combos,
                                Description = x.Description
                            };

                            SuperEntityLogic<Scheme> SchemeLogic = new SuperEntityLogic<Scheme>();
                            SchemeLogic.Insert(scheme);
                            SchemeLogic.Commit();
                            isSuccessful = true;
                            return isSuccessful;
                        }
                        catch (Exception)
                        {
                            isSuccessful = false;
                            return isSuccessful;
                        }
                })
                    .OnSuccessDisplay("Scheme saved successfully!!")
                    .OnFailureDisplay("An error occurred");
        }
    }
}
