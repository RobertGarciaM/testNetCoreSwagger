using AutoMapper;
using Data.Api.Models;
using Data.Api.Repositories.User;
using Domain.Api.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Api.Logic.UserSection
{
    public class UserLogic
    {
        /// <summary>
        /// Consulta en el repositorio los datos del usuario
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>objeto usuario</returns>
        public async Task<ApplicationUserViewModel> GetUser(string username)
        {
            try
            {
                var userRepository = new UserRepository();                
                return Mapper.Map<ApplicationUser , ApplicationUserViewModel>(await userRepository.GetUserByName(username));
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// Permite tranferir del balance del usuario logueado al balance de otro usuario
        /// </summary>
        /// <param name="fromUser"></param>
        /// <param name="transfer"></param>
        /// <returns></returns>
        public async Task<bool> TransferBalance(string fromUser, TransferViewModel transfer)
        {
            try
            {
                var userRepository = new UserRepository();
                var userFrom = await userRepository.GetUserByName(fromUser);
                if (userFrom.Balance < transfer.balance) { throw new Exception("Su fondos son insuficientes para tranferir"); }
                else
                {
                    var userTo = await userRepository.GetUserByName(transfer.userNameToTransfer);
                    if (userTo == null) { return false; }

                    userFrom.Balance = userFrom.Balance - transfer.balance;
                    await userRepository.Updateuser(userFrom);

                    userTo.Balance = userTo.Balance + transfer.balance;
                    await userRepository.Updateuser(userTo);
                }
                return true;
            }
            catch (Exception e) { throw e; }
        }        


    }
}
