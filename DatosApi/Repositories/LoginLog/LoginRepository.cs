using Data.Api.ApplicactionDbContextSpace;
using Data.Api.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.Repositories.LoginLog
{
    public class LoginRepository
    {
        /// <summary>
        /// Almacena en base datos el historico de login de los usuarios
        /// </summary>
        /// <param name="sedeUsuario"></param>
        /// <returns></returns>
        public async Task SaveLogLogin(LoginLogModel sedeUsuario)
        {
            using (ApplicactionDbContext Db = new ApplicactionDbContext())
            {
                await Db.AddAsync(sedeUsuario);
                await Db.SaveChangesAsync();
            }
        }
    }
}
