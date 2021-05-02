using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using YgorTeste.Controllers;
using YgorTeste.DAL;
using YgorTeste.Models;

namespace XUnitTestYgor
{
    public class UsuarioDALTest
    {
        IUsuarioDAL _usuarioDal;


        public UsuarioDALTest() 
        {
            _usuarioDal = new UsuarioDALServiceTest();
        }


        [Fact]
        public void UsuarioExists_VerificarExistenciaUsuario()
        {
            // Act 
            bool okResult = _usuarioDal.UsuarioExists(1);
            // Assert
            Assert.IsType<bool>(okResult);
            Assert.True(okResult);
        }

        [Fact]
        public void EmailExiste_UsuarioComEmail_ReturnaUsuario()
        {
            // Act 
            var okResult = _usuarioDal.EmailExiste("hello@world.com");
            // Assert
            Assert.IsType<List<Usuario>>(okResult);
            Assert.NotNull(okResult);
            Assert.NotEmpty(okResult);
        }

        [Fact]
        public void EmailExiste_EmailUnico()
        {
            // Act
            var okResult = _usuarioDal.EmailExiste("hello@world.com");

            //Descomentar para favorecer o teste com capitura de erro
            //Usuario usu = new Usuario();
            //usu.email = "hello@world.com";
            //usu.firstName = "Hello";
            //usu.lastName = "Word";
            //usu.password = "hunter";

            //okResult.Add(usu);

            // Assert
            var items = Assert.IsType<List<Usuario>>(okResult);
            Assert.Single(items);
            Assert.IsType<List<Usuario>>(okResult);
            Assert.NotNull(okResult);
            Assert.NotEmpty(okResult);
        }

    }
}
