using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Enum
{
    public enum TransactionType
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3,
        Purchase = 4,
        Sale = 5,
        Refund = 6,
        Fee = 7,
        Interest = 8,
        Adjustment = 9,
        BidPayment = 10,
        Other = 11
    }
}
