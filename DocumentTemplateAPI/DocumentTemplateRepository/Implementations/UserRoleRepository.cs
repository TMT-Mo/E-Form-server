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
    public class UserRoleRepository : BaseCRUDRepository<TemplateCreationRequest, TemplateUpdateRequest, UserRole, int>, IUserRoleRepository
    {
        public UserRoleRepository(IUnitOfWork _UnitOfWork, IDBRepositoryBase<UserRole> _repo)
             : base(_UnitOfWork, _repo)
        {
        }

        protected override UserRole CreationTypeToEdmx(TemplateCreationRequest inp)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateEDMXFromUpdateReq(UserRole edmx, TemplateUpdateRequest inp)
        {
            throw new NotImplementedException();
        }
    }
}
