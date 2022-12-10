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
    public class CategoryRepository : BaseCRUDRepository<TemplateCreationRequest, TemplateUpdateRequest, Category, int>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork _UnitOfWork, IDBRepositoryBase<Category> _repo)
             : base(_UnitOfWork, _repo)
        {
        }

        protected override Category CreationTypeToEdmx(TemplateCreationRequest inp)
        {
            throw new NotImplementedException();
        }
        protected override void UpdateEDMXFromUpdateReq(Category edmx, TemplateUpdateRequest inp)
        {
            throw new NotImplementedException();
        }
    }
}
