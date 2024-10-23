using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using Switcha.Core.Models;

namespace Switcha.UI.Model
{
    public class TransactionTypeUIModel: TransactionType
    {
        public virtual IList<TransactionType> TransactionTypes { get; set; }

    }
}
