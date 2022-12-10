using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocumentTemplateModel.Models
{
    public partial class Template
    {
        public Template()
        {
            UserTemplate = new HashSet<UserTemplate>();
        }

        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? Size { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdateBy { get; set; }
        public string Link { get; set; }
        public int IdUser { get; set; }
        public bool? IsEnable { get; set; }
        public int? IdType { get; set; }

        public virtual Category IdTypeNavigation { get; set; }
        public virtual ICollection<UserTemplate> UserTemplate { get; set; }
    }
}
