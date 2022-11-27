using DocumentTemplateModel.Entities;
using DocumentTemplateRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentTemplateUtilities;
using DocumentTemplateModel.Entities.Templates;

namespace DocumentTemplateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public TemplateController(ITemplateRepository _templateRepository, IDepartmentRepository _departmentRepository)
        {
            this._templateRepository = _templateRepository;
            this._departmentRepository = _departmentRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPagination()
        {
            var queries = HttpContext.Request;
            try
            {
                var departments = _departmentRepository.GetAll();
                var paginationParams = ConvertEDMXToDetail.ParsePaginationParams(queries);
                var result = _templateRepository.GetPagination(paginationParams.Page, paginationParams.Size, queries);

                return Ok(new PaginationResult<TemplateReponse>
                    {
                        Items = ConvertEDMXToDetailShared.TierEdmxToListDetails(result.Items),
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
