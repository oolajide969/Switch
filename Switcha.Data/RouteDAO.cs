using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;

namespace Switcha.Data
{
    public class RouteDAO : SuperEntityDAO<Route>
    {
        public Route GetRouteUsingBIN(string bin)
        {
            if (string.IsNullOrEmpty(bin))
            {
                return null;
            }
            else
            {
                var result = GetSession().Query<Route>().Where(r => r.CardPAN == bin).SingleOrDefault();
                return result;
            }
        }
    }
}
