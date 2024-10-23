using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;
using Switcha.Data;


namespace Switcha.Logic
{
    public class ChannelsLogic : SuperEntityLogic<Channels>
    {
        public Channels GetChannelUsingCode(string code)
        {
            return new ChannelsDAO().GetChannelUsingCode(code);
        }
    }
}
