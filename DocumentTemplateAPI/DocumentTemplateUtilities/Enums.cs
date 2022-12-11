using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateUtilities
{
    public static class Enums
    {
        public enum Status
        {
            ACTIVE = 1,
            DISABLE = 2
        }
        public enum ReponseUser
        {
            NOTFOUND = 1,
            INVALIDPASSWORD = 2
        }
        public enum StatusTemplate
        {
            New = 1,
            Approved = 2,
            Rejected = 3
        }
        public enum IsEnableTemplate
        {
            Enable = 1,
            Disabled = 2
        }
        public enum StatusDocument
        {
            Processing = 1,
            Approved = 2,
            Rejected = 3
        }
        public enum IsLockedDocument
        {
            Locked = 1,
            Unlocked = 2
        }
        public enum StatusSignatory
        {
            NotYet = 1,
            Processing = 2,
            Approved = 3,
            Rejected = 4
        }
    }
}
