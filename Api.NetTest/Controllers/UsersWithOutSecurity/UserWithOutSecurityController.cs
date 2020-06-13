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
using static Api.NetTest.Enums.Enums;

namespace Api.NetTest.Controllers.UsersWithOutSecurity
{
    public class UserWithOutSecurityController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _applicationSettings;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserWithOutSecurityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
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
        /// (NO TIENE SEGURIDAD, SE CREO PARA QUE CREE EL PRIMER USUARIO SIN ESTAR LOGUEADO)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
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
        /// Acción que crea roles en la aplicación (PERMITE CREAR UN ROL SE CREO PARA QUE CREE EL PRIMER USUARIO SIN ESTAR LOGUEADO)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Role")]
        public async Task<JsonResult> CreateRole(RoleViewModel model)
        {
            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var role = await _roleManager.FindByNameAsync(model.Name);
                if (role != null) { response.Data = "The role already exists"; return Json(response);  }

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
    }
}