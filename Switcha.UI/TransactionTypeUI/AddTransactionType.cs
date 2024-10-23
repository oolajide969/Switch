using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.Model;

namespace Switcha.UI.TransactionTypeUI
{
    public class AddTransactionType : EntityUI<TransactionTypeUIModel>
    {
        public AddTransactionType()
        {
            WithTitle("Transaction Types"); //main page title

            Map(x => x.Name).As<TextBox>()
                .WithLength(50)
                .LabelTextIs("Transaction's name") //a custom label
                .Required()         //a required field
                .TextFormatIs(@"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$");  //regex for name. You can validate any textbox using the same procedure
            
            Map(x => x.Code).As<TextBox>()
                .Required()
                .WithLength(2) 
                .TextFormatIs(TextFormat.numeric); 

            Map(x => x.Description).As<TextArea>()
                .Required();

            AddButton()
                .WithText("Save")
                .ConfirmWith("Sure?")
                .SubmitTo(x =>
                {
                    bool isSuccessful = false;
                    try
                    {
                        TransactionType transactionType = new TransactionType()
                        {
                            Name = x.Name,
                            Code = x.Code,
                            Description = x.Description
                        };

                        bool uniqueCode = CompareCode(transactionType.Code);

                        if (uniqueCode == true)
                        {
                            SuperEntityLogic<TransactionType> TransactionTypeLogic = new SuperEntityLogic<TransactionType>();
                            TransactionTypeLogic.Insert(transactionType);
                            TransactionTypeLogic.Commit();
                            isSuccessful = true;
                            return isSuccessful;
                        }
                            isSuccessful = false;
                            return isSuccessful;
                        
                    }
                    catch (Exception)
                    {
                        isSuccessful = false;
                        return isSuccessful;
                    }

                })
                .OnSuccessDisplay("Transaction Type saved successfully!!!")
                .OnFailureDisplay("Code already exists!!");
        }
        public bool CompareCode(string code)
        {
            TransactionType transactionType = new SuperEntityLogic<TransactionType>().GetAll().Where(x => x.Code == code).SingleOrDefault();
            if (transactionType == null)
            {
                return true;
            }
            return false;
        }
    }
}
