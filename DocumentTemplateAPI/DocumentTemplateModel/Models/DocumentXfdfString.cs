using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocumentTemplateModel.Models
{
    public partial class DocumentXfdfString
    {
        public int Id { get; set; }
        public int IdDocument { get; set; }
        public int IdXfdfString { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdateBy { get; set; }
        public int? Queue { get; set; }

        public virtual Document IdDocumentNavigation { get; set; }
        public virtual XfdfString IdXfdfStringNavigation { get; set; }
    }
}
