using System;
using System.Collections.Generic;
using Xunit;
using YgorTeste.DAL;
using YgorTeste.Models;

namespace XUnitTestYgor
{
    public class UsuarioDALServiceTest : IUsuarioDAL
    {
        public Usuario ObterUsuario(int id)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Test1()
        {

        }

        bool IUsuarioDAL.AtualizarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        bool IUsuarioDAL.CriarUsuario(Usuario usuario, List<phones> fone)
        {
            throw new NotImplementedException();
        }

        List<Usuario> IUsuarioDAL.EmailExiste(string email)
        {
            List<Usuario> usuarios  = new List<Usuario>();
            Usuario usu = new Usuario();           

            usu.email = "hello@world.com";
            usu.firstName = "Hello";
            usu.lastName = "Word";
            usu.password = "hunter";

            usuarios.Add(usu);

            return usuarios;
        }



        Usuario IUsuarioDAL.ObterUsuario(string email, string password)
        {
            throw new NotImplementedException();
        }

        bool IUsuarioDAL.UsuarioExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
