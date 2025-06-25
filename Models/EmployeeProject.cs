using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityframeWorkCore.Models
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }
        public Employee Employees { get; set; }


        public int ProjectId {get; set; } 
        public Project Projects { get; set; }    
    }
}
