using AutoMapper;
using Data.Api.Models;
using Data.Api.Repositories.LoginLog;
using Data.Api.Repositories.User;
using Domain.Api.ViewModels.Login;
using Domain.Api.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Api.Logic
{
    public class AdminLogic
    {
        private readonly LoginRepository _LoginRepository = new LoginRepository();
        /// <summary>
        /// Almacena el log del login del usuario
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task SaveLogLogin(string userName)
        {
            try
            {
                LogLoginViewModel logLogin = new LogLoginViewModel
                {
                    userName = userName,
                    dateLogin = DateTime.Now
                };
                var logLoginModel = Mapper.Map<LogLoginViewModel, LoginLogModel>(logLogin);
                await _LoginRepository.SaveLogLogin(logLoginModel);               
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Retorna el listado de usuarios de la aplicación
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApplicationUserViewModel>> GetUserApp()
        {
            try
            {
                AdminRepository _UserRepository = new AdminRepository();
                return await _UserRepository.GetUsers();
            }
            catch (Exception e) { throw e; }
        }


        /// <summary>
        /// Permite actualizar un usuario especifico
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateUser(ApplicationUserViewModel model)
        {
            try
            {
                UserRepository _UserRepository = new UserRepository();
                var user = await _UserRepository.GetUserByName(model.UserName);
                user.UserName = model.UserName;
                user.CompletName = model.CompletName;
                user.Age = model.Age;
                user.Nationality = model.Nationality;

                await _UserRepository.Updateuser(user);
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Actualiza el balance de un usuario puede ser agregar o reducir, depende del signo del balance que recibe
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ManagementBalance(ManageBalanceViewModel model)
        {
            try
            {
                UserRepository _userRepository = new UserRepository();
                var user = await _userRepository.GetUserByName(model.username);
                user.Balance = user.Balance + model.balanceToAdd;
                await _userRepository.Updateuser(user);                
            }
            catch (Exception e) { throw e; }
        }
    }
}
