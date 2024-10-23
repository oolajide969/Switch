using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;

namespace Switcha.Data
{
    public class ChannelsDAO : SuperEntityDAO<Channels>
    {
        public Channels GetChannelUsingCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return null;
            }
            else
            {
                return GetSession().QueryOver<Channels>().Where(x => x.Code == code).SingleOrDefault();
            }
        }

    }

}
