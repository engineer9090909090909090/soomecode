using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AliHelper
{
    public class AliHelperException : Exception
    {
    }

    public class PasswordException : Exception
    {
        public PasswordException(): base()
        {
        }
        public PasswordException(string message)  : base(message)
        {
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException() : base()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }
    }
}
