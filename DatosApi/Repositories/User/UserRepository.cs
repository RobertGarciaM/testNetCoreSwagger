using Data.Api.ApplicactionDbContextSpace;
using Data.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Api.Repositories.User
{
    public class UserRepository
    {
        /// <summary>
        /// Retorna desde la base de datos los datos del usuario que recibe
        /// </summary>
        /// <param name="username"></param>
        /// <returns>usuario</returns>
        public async Task<ApplicationUser> GetUserByName(string username)
        {
            using (ApplicactionDbContext Db = new ApplicactionDbContext())
            {
                return await Db.ApplicationUser.Where(x => x.UserName == username).FirstOrDefaultAsync();                
            }
        }

        /// <summary>
        /// Actualiza los datos del usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Updateuser(ApplicationUser user)
        {
            using (ApplicactionDbContext Db = new ApplicactionDbContext())
            {
                Db.Entry(user).State = EntityState.Modified;
                await Db.SaveChangesAsync();
            }
        }
    }
}
