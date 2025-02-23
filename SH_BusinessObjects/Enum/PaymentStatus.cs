using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Enum
{
    public enum PaymentMethod
    {
        COD = 1,
        BankCard = 2,
        PayPal = 3,
    }

    public enum PaymentStatus
    {
        Complete = 1,
        Pending = 0,
    }
}
