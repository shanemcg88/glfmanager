using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.App.Exceptions
{
    public class JobDescriptionsDoesNotEqualPositionsOpenException : Exception
    {
        public JobDescriptionsDoesNotEqualPositionsOpenException(string message) : base(message) { }
    }
}
