using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityframeWorkCore.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public EmployeeDetails Details { get; set; }
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        public ICollection<EmployeeProject> EmployeeProjects { get; set; }

    }
}
