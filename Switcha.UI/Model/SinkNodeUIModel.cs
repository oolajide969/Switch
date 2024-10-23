using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;

namespace Switcha.UI.Model
{
    public class SinkNodeUIModel : SinkNode
    {
        public virtual IList<SinkNode> SInkNodeList { get; set; }

    }
}
