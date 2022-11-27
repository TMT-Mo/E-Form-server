using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocumentTemplateModel.Models
{
    public partial class Document
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public string Version { get; set; }
        public int? StatusDocument { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? IdUser { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }

        public virtual User IdUserNavigation { get; set; }
    }
}
