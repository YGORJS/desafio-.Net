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
        private readonly IphonesDAL _phonesDAL;


        public UsuarioDAL(ApiContext context, IphonesDAL phonesDAL)
        {
            _context = context;
            _phonesDAL = phonesDAL;
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
            if(usuario != null)
            {
                foreach(phones p in _phonesDAL.ObterFonesUsuario(usuario.Id).ToList())
                {
                    usuario.phones.Add(p);

                }
                usuario.phones = usuario.phones.GroupBy(x => x.number).Select(x => x.First()).ToList();

            }
            return usuario;
        }

        public bool CriarUsuario(Usuario usuario, List<phones> fone)
        {
            try
            {
                _context.Usuarios.Add(usuario);

                foreach (phones fones in usuario.phones)
                {
                    fones.usuarioid = usuario.Id;
                    _context.Fone.Add(fones);

                }
                _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public Usuario ObterUsuario(int id)
        {
            return _context.Usuarios.Find(id);
        }
    }
}
