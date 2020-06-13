using System;
using Data.Api.Helpers;
using Data.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Api.ApplicactionDbContextSpace
{
    public class ApplicactionDbContext : IdentityDbContext
    {
        public ApplicactionDbContext() { }
        public ApplicactionDbContext(DbContextOptions<ApplicactionDbContext> options) : base(options) { }

        // <summary>
        /// Sobreescribimos el metodo OnConfiguring del DbContext para configurar como base de datos SqlServer 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(MySettingModel.GetNetTestConecction(), options =>
            {
                options.CommandTimeout(120);
            });
        }


        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<LoginLogModel> LoginLogModel { get; set; }

    }
}
