using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo.api.Exceptions
{
    public class BusinessException: Exception
    {

        public BusinessException(string message) : base(message)
        {
            
        }
        
    }
}