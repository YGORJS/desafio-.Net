using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YgorTeste.Context;
using Microsoft.EntityFrameworkCore;
using YgorTeste.DAL;
using YgorTeste.Context;
using YgorTeste.Models;
using YgorTeste.IBLL;
using YgorTeste.Models.DTO;

namespace YgorTeste.BLL
{
    public class UsuarioBLL:IUsuarioBLL
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

        public bool CadastrarUsuario(Usuario usuario, List<phones> fone)
        {
            usuario.createdAt = DateTime.Now;
            return _usuarioDAL.CriarUsuario(usuario,fone);

        }

        public UsuarioDTO BuscarUsuario(Usuario usuario, UsuarioDTO usuarioFormatado)
        {
            usuarioFormatado.phones.AddRange(usuario.phones);


            usuarioFormatado.firstName = usuario.firstName;
            usuarioFormatado.lastName = usuario.lastName;
            usuarioFormatado.email = usuario.email;
            usuarioFormatado.created_at = usuario.createdAt.ToString("MM/dd/yyyy HH:mm");
            usuarioFormatado.last_login = usuario.last_login.ToString("MM/dd/yyyy HH:mm");

            return usuarioFormatado;
        }
    }

}
