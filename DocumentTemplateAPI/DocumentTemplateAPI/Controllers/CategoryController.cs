using DocumentTemplateModel.Entities;
using DocumentTemplateRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentTemplateUtilities;
using DocumentTemplateModel.Entities.Templates;
using DocumentTemplateModel.Entities.Departments;

namespace DocumentTemplateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository _categoryRepository)
        {
            this._categoryRepository = _categoryRepository;
        }

        [HttpGet]
        [Route("getCategories")]
        public async Task<IActionResult> GetPagination()
        {
            var queries = HttpContext.Request;
            try
            {
                var departments = _categoryRepository.GetAll();
                var paginationParams = Helper.ParsePaginationParams(queries);
                var result = _categoryRepository.GetPagination(paginationParams.Page, paginationParams.Size, queries);

                return Ok(new PaginationResult<CategoryReponse>
                    {
                        Items = ConvertEDMXToDetailShared.CategoryEdmxToListDetails(result.Items),
                        Page = result.Page,
                        Total = result.Total,
                        Size = result.Size
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
