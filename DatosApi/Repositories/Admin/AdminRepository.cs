using Data.Api.ApplicactionDbContextSpace;
using Domain.Api.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data.Api.Repositories.User
{
    public class AdminRepository
    {

        /// <summary>
        /// Permite obtener los usuarios de la aplicación
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApplicationUserViewModel>> GetUsers()
        {
            using (ApplicactionDbContext Db = new ApplicactionDbContext())
            {

                var users = from u in Db.ApplicationUser
                            join ur in Db.UserRoles on u.Id equals ur.UserId
                            join r in Db.Roles on ur.RoleId equals r.Id
                            select new ApplicationUserViewModel
                            {
                                UserName = u.UserName,
                                CompletName = u.CompletName,
                                Age = u.Age,
                                Nationality= u.Nationality,
                                Rol = r.Name,
                                Balance = u.Balance
                            };

                return await users.Distinct().ToListAsync();
            }
        }
    }
}
