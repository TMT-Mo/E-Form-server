using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocumentTemplateModel.Models
{
    public partial class User
    {
        public User()
        {
            Document = new HashSet<Document>();
            UserRole = new HashSet<UserRole>();
            UserTemplate = new HashSet<UserTemplate>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Signature { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdateBy { get; set; }

        public virtual ICollection<Document> Document { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<UserTemplate> UserTemplate { get; set; }
    }
}
