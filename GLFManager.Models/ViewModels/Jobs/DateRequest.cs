using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.ViewModels.Jobs
{
    public class DateRequest
    {
        private DateTime _dateRequested;
        public DateTime DateRequested 
        {
            get => _dateRequested.Date;
            set => _dateRequested = value.Date;
        }
    }
}
