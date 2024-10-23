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
    public class AddChannels : EntityUI<ChannelsUIModel>
    {
        string errorMessage = "";
        public AddChannels()
        {
            WithTitle("Channels"); //main page title

            Map(x => x.Name).As<TextBox>()
                .WithLength(20)
                .LabelTextIs("Channel name") //a custom label
                .Required()         //a required field
                .TextFormatIs(@"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$");  //regex for name. You can validate any textbox using the same procedure

            Map(x => x.Code).As<TextBox>()
                .Required()
                .WithLength(1)
                .TextFormatIs(TextFormat.numeric);

            Map(x => x.Description).As<TextArea>()
                .Required();

            AddButton()
                .WithText("Create")
                .SubmitTo(x => 
                {
                    bool isSuccessful = false;
                    try
                    {
                        Channels channel = new Channels()
                        {
                            Name = x.Name,
                            Code = x.Code,
                            Description = x.Description
                        };

                        bool uniqueCode = CompareCode(channel.Code);
                        if (uniqueCode == true)
                        {
                            SuperEntityLogic<Channels> ChannelLogic = new SuperEntityLogic<Channels>();
                            ChannelLogic.Insert(channel);
                            ChannelLogic.Commit();
                            isSuccessful = true;
                            return isSuccessful;
                        }
                        errorMessage = "Channel with same code already exist!!";
                        isSuccessful = false;
                        return isSuccessful;
                    }
                    catch (Exception e)
                    {
                        errorMessage = "Contact your Administrator";
                        isSuccessful = false;
                        return isSuccessful;
                    }
                })
                .OnSuccessDisplay("Channel successfully saved!!")
                .OnFailureDisplay("An error occurred" + errorMessage);
        }
        public bool CompareCode(string code)
        {
            Channels channel = new SuperEntityLogic<Channels>().GetAll().Where(x => x.Code == code).SingleOrDefault();
            if (channel == null)
            {
                return true;
            }
            return false;
        }
    }
}

