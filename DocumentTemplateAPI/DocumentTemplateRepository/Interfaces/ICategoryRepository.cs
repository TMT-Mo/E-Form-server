using DocumentTemplateModel.Entities.Templates;
using DocumentTemplateModel.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateRepository.Interfaces
{
    public interface ICategoryRepository : ICrud<TemplateCreationRequest, TemplateUpdateRequest, DocumentTemplateModel.Models.Category, int>
    {

    }
}
