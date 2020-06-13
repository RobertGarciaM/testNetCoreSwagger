using Api.NetTest.Controllers.ControllerBase;
using Api.NetTest.Extensions;
using Api.NetTest.Helpers;
using Data.Api.ApplicationSettings;
using Data.Api.Models;
using Domain.Api.ViewModels.Login;
using Logic.Api.Logic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Api.NetTest.Enums.Enums;

namespace Api.NetTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController  : BaseController
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _applicationSettings;

        public JwtController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationSettings = appSettings.Value;
        }

        /// <summary>
        /// Controlador utilizado para loguear el usuario en la aplicación
        /// Después de loguearse inserte el token con la palabra "bearer" y luego el token ej: bearer token
        /// </summary>
        /// <param name="model"></param>        
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        //POST: /api/ApplicationUser/Login
        public async Task<JsonResult> Login(LoginViewModel model)
        {

            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var user = await _userManager.FindByNameAsync(model.userName);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.password))
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("Age", user.Age.ToString()),
                        new Claim("National", user.Nationality.ToString()),
                        new Claim("CompletName", user.CompletName.ToString())
                    };
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.ToString()));
                    }

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddHours(2),
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    };


                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token_api = tokenHandler.WriteToken(securityToken);

                    response.Data = new { token = token_api };

                    AdminLogic _UserLogic = new AdminLogic();
                    await _UserLogic.SaveLogLogin(user.UserName);
                }
                else
                {
                    response.Status = System.Net.HttpStatusCode.NotAcceptable; ;
                }
            }
            catch (Exception ex)
            {
                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Data = MensajesApplicacion.ErrorApplication.ToDescriptionString();
                response.Errors.Add(ex.Message);
            }

            return Json(response);
        }
    }
}
