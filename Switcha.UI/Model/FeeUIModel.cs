using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;

namespace Switcha.UI.Model
{
    public class FeeUIModel : Fee
    {
        public string Instruction { get; set; }

        public virtual IList<Fee> Fees { get; set; }
        public string ErrorMessage { get; set; }

        public static List<OptionAndValue> Options
        {
            get
            {
                return new OptionAndValue().GetOptionValues(typeof(FeeEnum));
            }
        }
    }
}
