using DocumentTemplateModel.Entities;
using DocumentTemplateModel.Entities.Users;
using DocumentTemplateRepository.Interfaces;
using DocumentTemplateUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentTemplateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UsersController(IUserRepository _userRepository, IDepartmentRepository _departmentRepository, IUserRoleRepository _userRoleRepository)
        {
            this._userRepository = _userRepository;
            this._departmentRepository = _departmentRepository;
            this._userRoleRepository = _userRoleRepository;
        }

        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest request)
        {
            //var invalidKey = ConvertEDMXToDetail.CheckWhiteListedParams(new List<string>());
            //if (invalidKey != null)
            //{
            //    return Request.CreateResponse<Response<dynamic>>(
            //        HttpStatusCode.BadRequest,
            //        new Response<dynamic>
            //        {
            //            Data = $"Not allowed parameter: { invalidKey }"
            //        }
            //    );
            //}
            try
            {
                var response = _userRepository.Authenticate(request.UserName, request.Password);
                if (response == Enums.ReponseUser.INVALIDPASSWORD.ToString() || response == Enums.ReponseUser.NOTFOUND.ToString())
                {
                    return Unauthorized(new Response
                    {
                        ErrorMessage = "UserName or password are incorrect"
                    });
                }

                var resp = Ok(new UserReponse
                {
                    Token = JwtHelpers.GenerateJwtToken(response)
                });

                return resp;
            }
            catch (Exception ex)
            {

                return StatusCode(500, new Response
                {
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetPagination()
        {
            var queries = HttpContext.Request;
            try
            {
                var query = _userRepository.GetAllQueryable();
                var userRoles = _userRoleRepository.GetAllQueryable();

                string departmentId_eq = queries.Query["departmentId_eq"];
                if (departmentId_eq != null)
                {
                    var userIds = userRoles.Where(x => x.IdDepartment == int.Parse(departmentId_eq)).Select(y => y.Id);
                    query = query.Where(x => userIds.Contains(x.Id));
                }

                var paginationParams = Helper.ParsePaginationParams(queries);
                var result = _userRepository.GetPagination(paginationParams.Page, paginationParams.Size, queries,null,query);

                return Ok(new PaginationResult<UserProfileReponse>
                {
                    Items = ConvertEDMXToDetailShared.UserEdmxToListDetails(result.Items),
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

