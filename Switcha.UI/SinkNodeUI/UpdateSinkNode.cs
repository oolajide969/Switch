using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.Model;

namespace Switcha.UI.SinkNodeUI
{
    public class UpdateSinkNode : EntityUI<SinkNode>
    {
        public UpdateSinkNode()
        {
            AddSection()
              .WithTitle("Update Sink Node")
              .IsFormGroup()
              .WithColumns(new List<Column>() {
                    new Column(new List<IField> ()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name").TextFormatIs(TextFormat.name),
                        Map(x => x.HostName).AsSectionField<TextBox>().LabelTextIs("Host Name"),
                        Map(x => x.IPAddress).AsSectionField<TextBox>().LabelTextIs("IP Address"),
                        Map(x => x.Port).AsSectionField<TextBox>().LabelTextIs("Port"),
                        //Map(x => x.isActive).AsSectionField<RadioButton>().LabelTextIs("Status"),
                        //Map(x => x.isActive).AsSectionField<DropDownList>()
                        //.Of(new string [] {"Active","Inactive"})
                        //.ListOf(x=>x,x=>x).LabelTextIs("Status"),


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
                        SuperEntityLogic<SinkNode> SinkNodeLogic = new SuperEntityLogic<SinkNode>();
                        SinkNodeLogic.Update(x);
                        SinkNodeLogic.Commit();
                        isSuccessful = true;
                        return isSuccessful;
                    }
                    catch (Exception)
                    {
                        isSuccessful = false;
                        return isSuccessful;
                        throw;
                    }
                }).OnSuccessDisplay("Sink Node successfully Updated")
                  .OnFailureDisplay("An error occurred!");
        }
    }
}
