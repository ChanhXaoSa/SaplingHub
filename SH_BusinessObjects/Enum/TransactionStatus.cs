using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Enum
{
    public enum TransactionStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3,
        Reversed = 4,
        Refunded = 5,
        Cancelled = 6,
        Processing = 7,
        OnHold = 8,
        Settled = 9,
        Disputed = 10
    }
}
