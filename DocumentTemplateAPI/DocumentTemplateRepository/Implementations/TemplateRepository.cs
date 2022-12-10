using DocumentTemplateModel.Entities.Templates;
using DocumentTemplateModel.Models;
using DocumentTemplateRepository.Interfaces;
using DocumentTemplateUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentTemplateRepository.Implementations
{
    public class TemplateRepository : BaseCRUDRepository<TemplateCreationRequest, TemplateUpdateRequest, Template, int>, ITemplateRepository
    {
        public TemplateRepository(IUnitOfWork _UnitOfWork, IDBRepositoryBase<Template> _repo)
             : base(_UnitOfWork, _repo)
        {
        }

        protected override Template CreationTypeToEdmx(TemplateCreationRequest inp)
        {
            return new Template
            {
                Status = 1,
                Size = inp.Size,
                TemplateName = inp.TemplateName,
                Type = inp.Type,
                Description = inp.Description,
                Link = inp.Link,
                IsEnable = true,
                CreatedBy = inp.CreatedBy,
                IdType = inp.IdTemplateType
            };
        }
        protected override void UpdateEDMXFromUpdateReq(Template edmx, TemplateUpdateRequest inp)
        {
            throw new NotImplementedException();
        }
    }
}
