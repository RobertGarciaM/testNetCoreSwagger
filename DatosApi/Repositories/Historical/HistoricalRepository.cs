using Data.Api.ApplicactionDbContextSpace;
using Domain.Api.ViewModels.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data.Api.Repositories.Historical
{
    public class HistoricalRepository
    {
        /// <summary>
        /// Obtiene el listado historico del log de registro
        /// </summary>
        /// <returns></returns>
        public async Task<List<LogLoginViewModel>> GetHistorical()
        {

            using (ApplicactionDbContext Db = new ApplicactionDbContext())
            {

                var historical = from u in Db.LoginLogModel
                                 join p in Db.ApplicationUser on u.userName equals p.UserName
                            select new LogLoginViewModel {
                                userName = p.CompletName,
                                dateLogin = u.dateLogin
                            };

                return await historical.ToListAsync();
            }
        }
    }
}
