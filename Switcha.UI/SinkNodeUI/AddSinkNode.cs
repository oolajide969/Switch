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
    public class AddSinkNode : EntityUI<SinkNodeUIModel>
    {
        public AddSinkNode()
        {
            WithTitle("Sink Node"); //main page title

            AddSection()
                  .WithTitle("")
                  .IsFramed()         //Frames, Collapse buttons are neat features you can add to your section
                  .WithColumns(new List<Column>{
                    new Column(new List<IField>{

                        //Section2 > Column 1 >form element 1
                        Map(x => x.Name)
                        .AsSectionField<TextBox>()
                        .Required()
                        .TextFormatIs(@"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$"),  //regex for name. You can validate any textbox using the same procedure

                         Map(x => x.IPAddress)
                        .AsSectionField<TextBox>()
                        .TextFormatIs(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")
                        .Required()
                    }),

                    new Column(new List<IField>{
                        //Section2 > Column 2 >form element 2
                        Map(x => x.HostName)
                       .AsSectionField<TextBox>()
                       .Required()
                       .TextFormatIs(@"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$"), //regex for name. You can validate any textbox using the same procedure

                        Map(x => x.Port)
                       .AsSectionField<TextBox>()
                       .TextFormatIs(TextFormat.numeric)
                       .WithLength(4)
                       .Required(),

                         Map(x=>x.Status)
                        .AsSectionField<DropDownList>()
                        .Of(new string [] {"Active","Inactive"})
                        .ListOf(x=>x,x=>x)
                        .LabelTextIs("Status"),

                       //Section2 > Column 2 >form element 2
                       AddSectionButton()
                       .WithText("Create")
                       .SubmitTo(x =>
                       {
                            bool isSuccessful = false;
                            try
                            {
                                    SinkNode sinknode = new SinkNode()
                                    {
                                        Name = x.Name,
                                        HostName = x.HostName,
                                        IPAddress = x.IPAddress,
                                        Port = x.Port,
                                        Status = x.Status
                                    };

                                    SuperEntityLogic<SinkNode> SinkNodeLogic = new SuperEntityLogic<SinkNode>();
                                    SinkNodeLogic.Insert(sinknode);
                                    SinkNodeLogic.Commit();
                                    isSuccessful = true;
                                    return isSuccessful;
                            }
                            catch (Exception)
                            {
                                 isSuccessful = false;
                                 return isSuccessful;
                            }
                       })
                       .OnSuccessDisplay("Sink Node saved successfully!!")
                       .OnFailureDisplay("Try Again. An error occurred!!")
                    }),

                  });
        }
    }
}
