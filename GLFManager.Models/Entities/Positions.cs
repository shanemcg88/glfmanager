using System;
using System.Collections.Generic;
using System.Text;

namespace GLFManager.Models.Entities
{
    public class Positions
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public decimal Rate { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}
