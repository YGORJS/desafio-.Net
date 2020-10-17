using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Context;
using Microsoft.EntityFrameworkCore;
using YgorTeste.DAL;
using YgorTeste.Context;
using YgorTeste.Models;

namespace YgorTeste.BLL
{
    public class UsuarioBLL
    {
        private readonly IUsuarioDAL _usuarioDAL ;


        public UsuarioBLL(IUsuarioDAL usuarioDAL)
        {
            _usuarioDAL = usuarioDAL;
        }


        public bool EmailExiste(string email)
        {

            var existe = _usuarioDAL.EmailExiste(email);

            if (existe.Count() > 0)
                return true;
            else
                return false;
            
        }

        public Usuario ObterUsuario( string email, string password) {

            Usuario usu = _usuarioDAL.ObterUsuario(email, password);

            return usu;
        }

        public bool AtualizarUsuario(Usuario usuario)
        {
              usuario.last_login = DateTime.Now;
              return  _usuarioDAL.AtualizarUsuario(usuario);      
          
        } 

    }

}
