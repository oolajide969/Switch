using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;

namespace Switcha.UI.Model
{
    public class ComboUIModel : Combo
    {
        public virtual IList<Combo> ComboList { get; set; }

    }
}
