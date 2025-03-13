using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_DataAccessObjects.Common.Exceptions
{
    public class UnauthorizedException(string message) : Exception(message)
    {
    }
}
