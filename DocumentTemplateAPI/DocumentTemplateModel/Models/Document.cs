﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocumentTemplateModel.Models
{
    public partial class Document
    {
        public Document()
        {
            DocumentXfdfString = new HashSet<DocumentXfdfString>();
        }

        public int Id { get; set; }
        public string DocumentName { get; set; }
        public string Version { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdateBy { get; set; }
        public string Type { get; set; }
        public int? Size { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string Comment { get; set; }
        public string TypesName { get; set; }
        public int IdUser { get; set; }
        public bool? IsLocked { get; set; }

        public virtual User IdUserNavigation { get; set; }
        public virtual ICollection<DocumentXfdfString> DocumentXfdfString { get; set; }
    }
}
