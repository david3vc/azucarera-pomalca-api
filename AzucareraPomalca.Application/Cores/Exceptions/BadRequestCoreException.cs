using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Cores.Exceptions
{
    public class BadRequestCoreException : Exception
    {
        public BadRequestCoreException(string message) : base(message)
        {
        }
    }
}
