using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.Model;

namespace Switcha.UI.ChannelsUI
{
    public class UpdateChannels : EntityUI<Channels>
    {
        public UpdateChannels()
        {
            AddSection()
               .WithTitle("Update Channel")
               .IsFormGroup()
               .WithColumns(new List<Column>() {
                    new Column(new List<IField> ()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().LabelTextIs("Name").TextFormatIs(TextFormat.name),
                        Map(x => x.Code).AsSectionField<TextBox>().LabelTextIs("Code").TextFormatIs(TextFormat.numeric),  
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
                        SuperEntityLogic<Channels> ChannelLogic = new SuperEntityLogic<Channels>();
                        ChannelLogic.Update(x);
                        ChannelLogic.Commit();
                        isSuccessful = true;
                        return isSuccessful;
                    }
                    catch (Exception)
                    {
                        isSuccessful = false;
                        return isSuccessful;
                        throw;
                    }
                }).OnSuccessDisplay("Channel Successfully Updated")
                  .OnFailureDisplay("An error occurred!");
        }

    }
}

