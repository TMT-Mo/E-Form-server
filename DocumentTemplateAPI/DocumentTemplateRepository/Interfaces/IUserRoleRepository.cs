using DocumentTemplateModel.Entities.Templates;
using DocumentTemplateModel.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateRepository.Interfaces
{
    public interface IUserRoleRepository : ICrud<TemplateCreationRequest, TemplateUpdateRequest, DocumentTemplateModel.Models.UserRole, int>
    {

    }
}
