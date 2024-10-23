using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;

namespace Switcha.UI.Model
{
    public class SourceNodeUIModel : SourceNode
    {
        public virtual IList<SourceNode> SourceNodeList { get; set; }

    }
}
