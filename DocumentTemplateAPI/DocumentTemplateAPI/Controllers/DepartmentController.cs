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
    public class DepartmentController : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(ITemplateRepository _templateRepository, IDepartmentRepository _departmentRepository)
        {
            this._templateRepository = _templateRepository;
            this._departmentRepository = _departmentRepository;
        }

        [HttpGet]
        [Route("getDepartments")]
        public async Task<IActionResult> GetPagination()
        {
            var queries = HttpContext.Request;
            try
            {
                var departments = _departmentRepository.GetAll();
                var paginationParams = Helper.ParsePaginationParams(queries);
                var result = _departmentRepository.GetPagination(paginationParams.Page, paginationParams.Size, queries);

                return Ok(new PaginationResult<DepartmentReponse>
                    {
                        Items = ConvertEDMXToDetailShared.DepartmentEdmxToListDetails(result.Items),
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
