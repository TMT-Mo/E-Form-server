using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocumentTemplateModel.Models
{
    public partial class UserTemplate
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdTemplate { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        public virtual Template IdTemplateNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
