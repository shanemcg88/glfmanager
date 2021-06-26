using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.App.Exceptions
{
    public class NoPositionsOpenException : Exception
    {
        public NoPositionsOpenException(string message) : base(message) { }
    }
}
