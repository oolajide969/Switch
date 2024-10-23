using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;
using Switcha.Data;

namespace Switcha.Logic
{
    public class RouteLogic : SuperEntityLogic<Route>
    {
        public Route GetRouteUsingBIN(string bin)
        {
            return new RouteDAO().GetRouteUsingBIN(bin);
        }
    }
}
