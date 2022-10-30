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

        public UsersController(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
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
                    return Unauthorized(new Response<dynamic>
                    {
                        ErrorMessage = "UserName or password are incorrect"
                    });
                }

                var resp = Ok(new Response<dynamic>
                {
                    Data = new UserReponse
                    {
                        Token = JwtHelpers.GenerateJwtToken(response)
                    }
                });

                return resp;
            }
            catch (Exception ex)
            {

                return StatusCode(500, new Response<dynamic>
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}

