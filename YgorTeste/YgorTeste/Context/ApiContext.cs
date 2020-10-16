using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YgorTeste.Models;

namespace YgorTeste.Context
{
    public class ApiContext: DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
        { }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
