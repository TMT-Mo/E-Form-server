using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocumentTemplateModel.Models
{
    public partial class Department
    {
        public Department()
        {
            Template = new HashSet<Template>();
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public virtual ICollection<Template> Template { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
