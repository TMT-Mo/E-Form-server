using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateUtilities
{
    public class ParameterNotAllowException : Exception
    {
        public ParameterNotAllowException()
        {
        }

        public ParameterNotAllowException(string message)
            : base(message)
        {
        }

        public ParameterNotAllowException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
