using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;
using Switcha.Logic;
using Switcha.UI.Model;

namespace Switcha.UI.FeeUI
{
	public class AddFee : EntityUI<FeeUIModel>
	{
		public AddFee()
		{
			//Use Prepopulate from when you want to initialize some members in your Ui model based on some info u 
			//get when the page is being called (e.g a request query string).
			//Here is an example
			PrePopulateFrom<FeeUIModel>(x =>
			{
				return new FeeUIModel
				{
					Instruction = "Fees should either be a" + "<br/>" + "flat fee or percentage which may be zero." + "<br/>" + "Fees should have options for absolute maximum or minimum"
				};
			});

			WithTitle("Fee Types"); //main page title

			AddSection()
				.WithTitle("General Info")
				.WithFields(new List<IField>
				{
					Map(x => x.Name).AsSectionField<TextBox>()
					.WithLength(50)
					.LabelTextIs("Fee's name") //a custom label
					.Required()         //a required field
					.TextFormatIs(@"^[A-Za-z\s]{1,}[\.]{0,1}[A-Za-z\s]{0,}$"), //regex for name. You can validate any textbox using the same procedure

					Map(x => x.FeeOptions)
					.AsSectionField<DropDownList>()
					.Of<OptionAndValue>(() => //supply the data that makes up the dropdown list. Typically this will be from a DataBase
					{
						return FeeUIModel.Options;
					})
					.ListOf(x => x.Option, x => x.Value), //identify which fields of the supplied data is the name and value field respectively         

					Map(x => x.Amount).AsSectionField<TextBox>()
					.WithLength(13)
					.Required()         //a required field
					.TextFormatIs(TextFormat.numeric)

				});

			AddSection()
				 .WithTitle("Amount Info")
				 .IsFramed()         //Frames, Collapse buttons are neat features you can add to your section
				 .IsCollapsible()
				 .WithColumns(new List<Column>{
					new Column(new List<IField>{

						//Section2 > Column 1 >form element 1
						Map(x => x.Minimum)
						.AsSectionField<TextBox>()
						.Required()
						.TextFormatIs(TextFormat.numeric),
					}),

					new Column(new List<IField>{
						//Section2 > Column 2 >form element 2
						Map(x => x.Maximum)
							.AsSectionField<TextBox>()
							.Required()
							.TextFormatIs(TextFormat.numeric),

						//Section2 > Column 2 >form element 2
						AddSectionButton()
							.WithText("Create")
							.SubmitTo(x =>
							{
								bool isSuccessful = false;
								try
								{
									Fee fee = new Fee()
									{
										Name = x.Name,
										Minimum = x.Minimum,
										Maximum = x.Maximum,
										FeeOptions = x.FeeOptions,
										Amount = x.Amount,
									};
									if (x.Maximum > x.Minimum)
									{
										SuperEntityLogic<Fee> FeeLogic = new SuperEntityLogic<Fee>();
										FeeLogic.Insert(fee);
										FeeLogic.Commit();
										isSuccessful = true;
										return isSuccessful;
									}
									else
									{
										isSuccessful = false;
										x.ErrorMessage = "Maximum Value is less than Minimum value";
										return isSuccessful;
									}
								}
								catch (Exception)
								{
									isSuccessful = false;
									x.ErrorMessage = "Contact your Administrator";
									return isSuccessful;
								}
							})
							.OnSuccessDisplay("Fee saved successfully!!")
							.OnFailureDisplay(x=> {return "An error occurred. "+x.ErrorMessage; } )
					}),
				 });
		}
		//public bool CompareFee()
		//{
		//	TransactionType transactionType = new SuperEntityLogic<TransactionType>().GetAll().Where(x => x.Code == code).SingleOrDefault();
		//	if (transactionType == null)
		//	{
		//		return true;
		//	}
		//	return false;
		//}

	}
	
}
