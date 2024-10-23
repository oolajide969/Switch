using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;

namespace Switcha.Data
{
        public class TransactionTypeDAO : SuperEntityDAO<TransactionType>
        {
            public TransactionType GetTransactionTypeUsingCode(string code)
            {
                if (string.IsNullOrEmpty(code))
                {
                    return null;
                }
                else
                {
                    return GetSession().Query<TransactionType>().Where(x => x.Code == code).SingleOrDefault();
                }
            }
        }
}
