using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YgorTeste.BLL;
using YgorTeste.Context;
using YgorTeste.DAL;
using YgorTeste.IBLL;
using YgorTeste.Models;
using YgorTeste.Models.DTO;

namespace YgorTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginUsuariosController : ControllerBase
    {
        private readonly UsuarioDTO _usuarioDTO;
        private readonly PhonesDTO _foneDTO;
        private readonly IphonesBLL _foneBLL;
        private readonly IUsuarioBLL _usuarioBLL;

        public LoginUsuariosController( UsuarioDTO usuarioDTO, IphonesBLL foneBLL, IUsuarioBLL usuarioBLL, PhonesDTO foneDTO)
        {
            _usuarioDTO = usuarioDTO;
            _foneBLL = foneBLL;
            _usuarioBLL = usuarioBLL;
            _foneDTO = foneDTO;
            _usuarioDTO.phones = new List<phones>();
        }

        


   

        // POST: api/LoginUsuarios
        [HttpPost]
        [Route("Signin")]
        public IActionResult PostLoginUsuario([FromBody] LoginUsuario loginUsuario)
        {

            try
            {
          
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var usuario = _usuarioBLL.ObterUsuario(loginUsuario.email, loginUsuario.password);

                if (usuario == null)
                {                    
                    return NotFound(new { message = "Invalid e-mail or password", errorcode = (int)HttpStatusCode.NotFound });
                }
                

               
                _usuarioBLL.AtualizarUsuario(usuario);


                return Ok(new { message = "Login realizado com sucesso" , codigo= (int)HttpStatusCode.OK, usuario = _usuarioBLL.BuscarUsuario(usuario, _usuarioDTO) });

            }catch(Exception e)
            {                
                return NotFound( new { message = e.Message , errorCode = (int)HttpStatusCode.NotFound });
            }


        }

        // POST: api/LoginUsuarios
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("me")]
        public IActionResult PostLoginUsuarioToken([FromBody] LoginUsuario loginUsuario)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var usuario = _usuarioBLL.ObterUsuario(loginUsuario.email, loginUsuario.password);

                if (usuario == null)
                {                   
                    return NotFound(new { message = "Invalid e-mail or password" , errorcode= (int)HttpStatusCode.NotFound });
                }
              
                               

                _usuarioBLL.AtualizarUsuario(usuario);

               

                return Ok(new { usuario = _usuarioBLL.BuscarUsuario(usuario, _usuarioDTO) });

            }
            catch (Exception e)
            {               
                return NotFound(new {message=e.Message, errorcode= (int)HttpStatusCode.NotFound });
            }


        }

     

    }
}