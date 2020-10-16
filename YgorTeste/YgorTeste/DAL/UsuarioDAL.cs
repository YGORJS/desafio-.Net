using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Context;
using YgorTeste.Models;

namespace YgorTeste.DAL
{
    public class UsuarioDAL
    {
        private readonly ApiContext _context;


        public UsuarioDAL(ApiContext context)
        {
            _context = context;
        }

        public List<Usuario> EmailExiste(string email)
        {
            var existe = _context.Usuarios.Where(a => a.Email.Equals(email)).ToList();

            return existe;
        }


    }
}
