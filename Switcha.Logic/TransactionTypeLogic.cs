using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;
using Switcha.Data;

namespace Switcha.Logic
{
    public class TransactionTypeLogic : SuperEntityLogic<TransactionType>
    {
        public TransactionType GetTransactionTypeUsingCode(string code)
        {
            TransactionTypeDAO transactionType = new TransactionTypeDAO();
            return transactionType.GetTransactionTypeUsingCode(code);

        }
    }
}
