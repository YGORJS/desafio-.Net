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

        public bool EmailExiste(string email, ApiContext context)
        {
            UsuarioDAL usuDAL = new UsuarioDAL(context);

            var existe = usuDAL.EmailExiste(email);

            if (existe.Count() > 0)
                return true;
            else
                return false;
            
        }

        public Usuario ObterUsuario( string email, string password  , ApiContext context) {

            UsuarioDAL usuDal = new UsuarioDAL(context);

            Usuario usu = usuDal.ObterUsuario(email, password);

            return usu;
        }

        public bool AtualizarUsuario(Usuario usuario, ApiContext context)
        {
              usuario.last_login = DateTime.Now;
                UsuarioDAL usuDal = new UsuarioDAL(context);
              return  usuDal.AtualizarUsuario(usuario);      
          
        } 

    }

}
