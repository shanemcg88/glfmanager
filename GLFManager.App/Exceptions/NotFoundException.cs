using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.App.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }


    }
}
