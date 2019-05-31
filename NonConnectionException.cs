using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ls.Core
{
    class NonConnectionException:Exception
    {
        public NonConnectionException(string mes, Exception innerException)
            : base(mes, innerException) 
        {
            
        }
    }
}
