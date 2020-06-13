using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.NetTest.Controllers.ControllerBase;
using Api.NetTest.Extensions;
using Api.NetTest.Helpers;
using Domain.Api.ViewModels.Users;
using Logic.Api.Logic.UserSection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Api.NetTest.Enums.Enums;

namespace Api.NetTest.Controllers.UserSection
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {

        /// <summary>
        /// Este endpoint permite consultar los datos del usuario que esta logueado en la aplicación
        /// </summary>
        /// <returns>Datos del usuario</returns>
        [HttpGet]
        [Route("GetUserApplication")]
        public async Task<JsonResult> GetUser()
        {

            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var userLogic = new UserLogic();
                response.Data = await userLogic.GetUser(User.Identity.Name);
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
        /// Permite transferir fondos del balance del usuario actual al balance del username que se indica en el viewmodel
        /// </summary>
        /// <param name="model">Balance a transferir y usuario al que se le va a tranferir</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Transfer")]
        public async Task<JsonResult>BalanceTransfer(TransferViewModel model)
        {

            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                var userLogic = new UserLogic();
                response.Data = new { transferSucces = await userLogic.TransferBalance(User.Identity.Name, model) };
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