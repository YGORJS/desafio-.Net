using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Context;
using YgorTeste.Models;

namespace YgorTeste.DAL
{
    public class UsuarioDAL: IUsuarioDAL
    {
        private readonly ApiContext _context;


        public UsuarioDAL(ApiContext context)
        {
            _context = context;
        }

        public List<Usuario> EmailExiste(string email)
        {
            var existe = _context.Usuarios.Where(a => a.email.Equals(email)).ToList();

            return existe;
        }

        public  bool AtualizarUsuario(Usuario usuario)
        {            
            try
            {
                if (!UsuarioExists(usuario.Id))
                    return false;

                _context.Entry(usuario).State = EntityState.Modified;
                _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }


        public Usuario ObterUsuario(string email, string password)
        {
            Usuario usuario  = _context.Usuarios.Where(a => a.email.Equals(email) && a.password.Equals(password)).FirstOrDefault();

            return usuario;
        }


    }
}
