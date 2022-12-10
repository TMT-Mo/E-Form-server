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
        private readonly IUserTemplateRepository _userTemplateRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public TemplateController(ITemplateRepository _templateRepository, IDepartmentRepository _departmentRepository, 
            IUserTemplateRepository _userTemplateRepository, ICategoryRepository _categoryRepository,
            IUserRepository _userRepository)
        {
            this._templateRepository = _templateRepository;
            this._departmentRepository = _departmentRepository;
            this._userTemplateRepository = _userTemplateRepository;
            this._categoryRepository = _categoryRepository;
            this._userRepository = _userRepository;
        }

        [HttpGet]
        [Route("gettemplates")]
        public async Task<IActionResult> GetPagination()
        {
            var queries = HttpContext.Request;
            try
            {
                var users = _userRepository.GetAll(); 
                var userTemplates = _userTemplateRepository.GetAll();
                var categories = _categoryRepository.GetAll();
                var paginationParams = ConvertEDMXToDetail.ParsePaginationParams(queries);
                var result = _templateRepository.GetPagination(paginationParams.Page, paginationParams.Size, queries);

                return Ok(new PaginationResult<TemplateReponse>
                    {
                        Items = ConvertEDMXToDetailShared.TemplateEdmxToListDetails(result.Items,categories, userTemplates, users),
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

        [HttpPost]
        [Route("create")]
        public IActionResult CreateNew([FromBody] TemplateCreationRequest request)
        {
            try
            {
                if (ModelState.IsValid && request != null)
                {
                    #region validation
                    var template = _templateRepository.GetAllQueryable();
                    var templateNameExist = template.FirstOrDefault(x => x.TemplateName == request.TemplateName);

                    if (templateNameExist != null)
                    {
                        return BadRequest(new Response
                        {
                            ErrorMessage = "This template is already existed!"
                        });
                    }
                    if (request.Size >= 20000)
                    {
                        return BadRequest(new Response
                        {
                            ErrorMessage = "This template is over 20MB!"
                        });
                    }
                    if (request.TemplateName.Length >= 50 && request.TemplateName.Length < 1)
                    {
                        return BadRequest(new Response
                        {
                            ErrorMessage = "File name has 50 as maxLength and 1 as minLength"
                        });
                    }
                    if (request.Description.Length >= 50)
                    {
                        return BadRequest(new Response
                        {
                            ErrorMessage = "This template is over 20MB!"
                        });
                    }
                    #endregion


                    //handdle type of file
                    var positionTypeName = request.TemplateName.LastIndexOf('.');
                    request.Type = request.TemplateName[positionTypeName..];

                    var templateCreation = _templateRepository.CreateNew(request);
                    foreach (var item in request.SignatoryList)
                    {
                        var userTemplate = new TemplateCreationRequest
                        {
                            IdTemplate = templateCreation.Id,
                            IdUser = item
                        };
                        _userTemplateRepository.CreateNew(userTemplate);

                    }
                    return Ok(new SucessResponse
                    {
                        Message = "This template is now waiting for an approval"
                    });
                }

                return Ok(new Response
                {
                    ErrorMessage = ConvertEDMXToDetailShared.GetErrorMessagesFromModalState(ModelState)
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
