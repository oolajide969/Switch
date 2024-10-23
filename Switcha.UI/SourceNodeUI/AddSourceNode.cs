using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.Model;

namespace Switcha.UI.SourceNodeUI
{
    public class AddSourceNode : EntityUI<SourceNodeUIModel>
    {
        public AddSourceNode()
        {
            WithTitle("Source Node"); //main page title

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

                        Map(x => x.Schemes)
                        .AsSectionField<MultiSelect>()
                        .Of<Scheme>(()=> new SuperEntityLogic<Scheme>().GetAll())
                        .WithColumn(x => x.Name)
                        //.WithColumn(x => x.Description)
                        .ListOf(x => x.Name, x => x.ID)
                        .Required()
                        .LabelTextIs("Scheme"),

                        

                       //Section2 > Column 2 >form element 2
                       AddSectionButton()
                       .WithText("Create")
                       .SubmitTo(x =>
                       {
                            bool isSuccessful = false;
                            try
                            {
                                    SourceNode sourceNode = new SourceNode()
                                    {
                                        Name = x.Name,
                                        HostName = x.HostName,
                                        IPAddress = x.IPAddress, 
                                        Port = x.Port,
                                        Schemes = x.Schemes,
                                        Status = x.Status
                                    };

                                    SuperEntityLogic<SourceNode> SourceNodeLogic = new SuperEntityLogic<SourceNode>();
                                    SourceNodeLogic.Insert(sourceNode);
                                    SourceNodeLogic.Commit();
                                    isSuccessful = true;
                                    return isSuccessful;
                            }
                            catch (Exception ex)
                            {
                               Debug.WriteLine(ex);
                                 isSuccessful = false;
                                 return isSuccessful;
                            }
                       })
                       .OnSuccessDisplay("Source Node saved successfully!!")
                       .OnFailureDisplay("Try Again. An error occurred!!!")
                    }),

                  });
        }
    }
}
