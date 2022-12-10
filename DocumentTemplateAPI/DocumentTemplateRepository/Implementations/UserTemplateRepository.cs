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
    public class UserTemplateRepository : BaseCRUDRepository<TemplateCreationRequest, TemplateUpdateRequest, UserTemplate, int>, IUserTemplateRepository
    {
        public UserTemplateRepository(IUnitOfWork _UnitOfWork, IDBRepositoryBase<UserTemplate> _repo)
             : base(_UnitOfWork, _repo)
        {
        }

        protected override UserTemplate CreationTypeToEdmx(TemplateCreationRequest inp)
        {
            return new UserTemplate
            {
                Status = 1,
                IdTemplate = inp.IdTemplate,
                IdUser = inp.IdUser
            };
        }

        protected override void UpdateEDMXFromUpdateReq(UserTemplate edmx, TemplateUpdateRequest inp)
        {
            throw new NotImplementedException();
        }
    }
}
