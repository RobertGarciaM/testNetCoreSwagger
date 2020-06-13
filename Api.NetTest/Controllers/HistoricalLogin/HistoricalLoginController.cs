using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.NetTest.Controllers.ControllerBase;
using Api.NetTest.Extensions;
using Api.NetTest.Helpers;
using Logic.Api.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Api.NetTest.Enums.Enums;

namespace Api.NetTest.Controllers.HistoricalLogin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistoricalLoginController : BaseController
    {
        /// <summary>
        /// Permite obtener el historial de logueos en la aplicación
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetHistorical")]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Historical()
        {

            var response = new JsonResultBody
            {
                Status = System.Net.HttpStatusCode.OK
            };

            try
            {
                HistoricalLogic _HistoricalLogic = new HistoricalLogic();
                response.Data = await _HistoricalLogic.GetHistorical();
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