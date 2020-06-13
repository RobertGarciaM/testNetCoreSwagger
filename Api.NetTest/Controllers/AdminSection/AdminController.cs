using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.NetTest.Controllers.ControllerBase;
using Api.NetTest.Extensions;
using Api.NetTest.Helpers;
using Data.Api.ApplicationSettings;
using Data.Api.Models;
using Domain.Api.ViewModels.Role;
using Domain.Api.ViewModels.Users;
using Logic.Api.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using static Api.NetTest.Enums.Enums;

namespace Api.NetTest.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {

        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _applicationSettings;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IOptions<ApplicationSettings> appSettings, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationSettings = appSettings.Value;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Permite crear los usuarios de la aplicación, primero debe crearse el rol y enviarsele en el correspondiente parametro.
        /// Al momento de crear un usuario, a este se le asigna de CONTRASEÑA  por defecto el nombre del usuario en minuscula.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterUser")]        
        public async Task<JsonResult> PostApplicationUser(ApplicationUserViewModel model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName.Trim().ToLower(),
                CompletName = model.CompletName,
                Age = (int)model.Age,
                Nationality = model.Nationality,
                Balance = model.Balance
            };

            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {

                var result = await _userManager.CreateAsync(applicationUser, applicationUser.UserName.Trim().ToLower());
                if (!result.Succeeded)
                {
                    response.Status = System.Net.HttpStatusCode.Conflict;
                    response.Data = "El usuario no se pudo crear compruebe el nombre quiza es demasiado corto o el usuario ya existe";
                }
                else
                {
                    response.Data = result;
                    var user = await _userManager.FindByNameAsync(applicationUser.UserName);
                    var resultRoles = await _userManager.AddToRoleAsync(user, model.Rol);
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


        /// <summary>
        /// Crea los roles de la aplicacion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterRole")]
        //POST: /api/GestionUsuarios/CrearRole
        public async Task<JsonResult> CreateRole(RoleViewModel model)
        {
            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                await _roleManager.CreateAsync(new IdentityRole(model.Name));
            }
            catch (Exception ex)
            {
                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Data = MensajesApplicacion.ErrorApplication.ToDescriptionString();
                response.Errors.Add(ex.Message);
            }

            return Json(response);
        }

        /// <summary>
        /// Consulta el listado completo de usuarios de la aplicacion
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUsers")]
        public async Task<JsonResult> GetUsers()
        {
            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                AdminLogic _UserLogic = new AdminLogic();
                response.Data = await _UserLogic.GetUserApp();
            }
            catch (Exception ex)
            {
                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Data = MensajesApplicacion.ErrorApplication.ToDescriptionString();
                response.Errors.Add(ex.Message);
            }

            return Json(response);
        }

        /// <summary>
        /// Permite actualizar el usuario, pero no actualiza el balance del mismo.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<JsonResult> UpdateUser(ApplicationUserViewModel user)
        {
            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var userFrom = await validateExistsUser(user.UserName);

                var role = await _userManager.GetRolesAsync(userFrom);
                await _userManager.RemoveFromRoleAsync(userFrom, role.FirstOrDefault());
                await _userManager.AddToRoleAsync(userFrom, user.Rol);
                AdminLogic _UserLogic = new AdminLogic();
                await _UserLogic.UpdateUser(user);

            }
            catch (Exception ex)
            {
                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Data = MensajesApplicacion.ErrorApplication.ToDescriptionString();
                response.Errors.Add(ex.Message);
            }

            return Json(response);
        }

        /// <summary>
        /// Permite elminar el usuario, con su rol asociado
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<JsonResult> DeleteUser(ApplicationUserDeleteViewModel user)
        {
            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var userFrom = await validateExistsUser(user.userName);
                await _userManager.DeleteAsync(userFrom);

            }
            catch (Exception ex)
            {
                response.Status = System.Net.HttpStatusCode.InternalServerError;
                response.Data = MensajesApplicacion.ErrorApplication.ToDescriptionString();
                response.Errors.Add(ex.Message);
            }

            return Json(response);
        }

        /// <summary>
        /// Valida la existencia de un usuario en la base de datos
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Nombre de usuario</returns>
        private async Task<ApplicationUser> validateExistsUser(string username)
        {
            var userFrom = await _userManager.FindByNameAsync(username);
            if (userFrom == null)
            {
                throw new Exception("El usuario no se encuentra en el sistema");
            }
            return userFrom;
        }

        /// <summary>
        /// Permite sumar o restar al balance de un usuari, Para restar al blance solo envie el valor antepuesto del signo menos ej: -200
        /// </summary>
        /// <param name="balance"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ManagmentBlance")]
        public async Task<JsonResult> ManagementBalance(ManageBalanceViewModel balance)
        {
            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                _ = await validateExistsUser(balance.username);
                var AdminLogic = new AdminLogic();
                await AdminLogic.ManagementBalance(balance);

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