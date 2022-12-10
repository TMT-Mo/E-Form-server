using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocumentTemplateModel.Models
{
    public partial class XfdfString
    {
        public XfdfString()
        {
            DocumentXfdfString = new HashSet<DocumentXfdfString>();
        }

        public int Id { get; set; }
        public string XfdfString1 { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdateBy { get; set; }

        public virtual ICollection<DocumentXfdfString> DocumentXfdfString { get; set; }
    }
}
