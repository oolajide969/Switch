using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;
using Switcha.Logic;

namespace Switcha.UI.TransactionTypeUI
{
    public class UpdateTransactionType : EntityUI<TransactionType>
    {
        public UpdateTransactionType()
        {
            AddSection()
                .WithTitle("Update Transaction Type")
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
                .WithText("Update!")
                .ConfirmWith("Sure you want to update?")
                .SubmitTo(x =>
                {
                    bool isSuccessful = false;
                    try
                    {
                        SuperEntityLogic<TransactionType> TransactionLogic = new SuperEntityLogic<TransactionType>();
                        TransactionLogic.Update(x);
                        TransactionLogic.Commit();
                        isSuccessful = true;
                        return isSuccessful;
                    }
                    catch (Exception)
                    {
                        //errorMsg = ex.Message.ToString();
                        isSuccessful = false;
                        return isSuccessful;
                        throw;
                    }
                }).OnSuccessDisplay("Transaction Type Successfully Updated")
                  .OnFailureDisplay("An error occurred!");
        }

        //public bool CheckCode(string code)
        //{
        //    Channel channel = new SuperEntityLogic<Channel>().GetAll().Where(x => x.Code == code).First();
        //    if (channel == null)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
