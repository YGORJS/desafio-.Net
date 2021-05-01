using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YgorTeste.Models;

namespace YgorTeste.Context
{
    public class ApiContext : IdentityDbContext<ApplicationUserToken>
    {
        public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<phones> Fone { get; set; }
        public DbSet<YgorTeste.Models.LoginUsuario> LoginUsuario { get; set; }
        public DbSet<ApplicationUserToken> ApplicationUserToken { get; set; }

     



    }
}
