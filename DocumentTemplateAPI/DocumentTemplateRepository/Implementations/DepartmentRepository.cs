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
    public class DepartmentRepository : BaseCRUDRepository<TemplateCreationRequest, TemplateUpdateRequest, Department, int>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork _UnitOfWork, IDBRepositoryBase<Department> _repo)
             : base(_UnitOfWork, _repo)
        {
        }

        protected override Department CreationTypeToEdmx(TemplateCreationRequest inp)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateEDMXFromUpdateReq(Department edmx, TemplateUpdateRequest inp)
        {
            throw new NotImplementedException();
        }
    }
}
