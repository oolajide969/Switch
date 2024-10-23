using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Logic;
using Switcha.Core.Models;
using Switcha.UI.Model;
using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;

namespace Switcha.UI.SourceNodeUI
{
    public class UpdateSourceNode : EntityUI<SourceNode>
    {
        public UpdateSourceNode()
        {
            AddSection()
                .WithTitle("Update Source Node")
                .IsFormGroup()
                .WithColumns(new List<Column>() {
                    new Column(new List<IField> ()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name"),//.TextFormatIs(TextFormat.alphanumeric),
                        Map(x => x.HostName).AsSectionField<TextBox>().LabelTextIs("Host Name"),//TextFormatIs(TextFormat.alphanumeric),  
                        Map(x => x.IPAddress).AsSectionField<TextBox>().LabelTextIs("IPAddress").TextFormatIs(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$"),
                        Map(x => x.Port).AsSectionField<TextBox>().LabelTextIs("Port").TextFormatIs(TextFormat.numeric),
                        Map(x => x.Status).AsSectionField<DropDownList>().Of(new string [] {"Active","Inactive"}).ListOf(x=>x,x=>x).LabelTextIs("Status"),
                        HasMany(x=>x.Schemes)
                        .AsSectionField<MultiSelect>()
                            .Of<Scheme>(()=> new SuperEntityLogic<Scheme>().GetAll())
                            .WithColumn(x=>x.Name)
                            .WithColumn(x => x.Description)
                            .ListOf(x => x.Name, x => x.ID)
                            .Required()
                            .LabelTextIs("Scheme"),                        
                    }),
                });

            AddButton()
                .WithText("Update!")
                .ConfirmWith("Sure you wanna update this?")
                .SubmitTo(x =>
                {
                    bool isSuccessful = false;
                    try
                    {
                        SuperEntityLogic<SourceNode> SourceNodeLogic = new SuperEntityLogic<SourceNode>();
                        SourceNodeLogic.Update(x);
                        SourceNodeLogic.Commit();
                        isSuccessful = true;
                        return isSuccessful;
                    }
                    catch (Exception)
                    {
                        isSuccessful = false;
                        return isSuccessful;
                        throw;
                    }
                }).OnSuccessDisplay("Source Node Successfully Updated!!!")
                  .OnFailureDisplay("Source Node NOT Updated!!!!!!!");
        }
    }
}
