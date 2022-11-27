using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocumentTemplateModel.Models
{
    public partial class Template
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public int? IdDepartment { get; set; }
        public int? StatusTemplate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }

        public virtual Department IdDepartmentNavigation { get; set; }
    }
}
