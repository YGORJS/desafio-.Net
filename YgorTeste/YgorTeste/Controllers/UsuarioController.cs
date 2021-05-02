using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YgorTeste.BLL;
using YgorTeste.Context;
using YgorTeste.DAL;
using YgorTeste.IBLL;
using YgorTeste.Models;

namespace YgorTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioBLL _usuarioBLL;


        public UsuarioController(IUsuarioBLL usuarioBLL)
        {
            _usuarioBLL = usuarioBLL;
        }

       

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public IActionResult GetUsuario([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario =  _usuarioBLL.ObterUsuario(id);


            if (usuario == null)
            {
                return NotFound();
            }


            return Ok(usuario);
        }



    
        // POST: api/Usuario
        [HttpPost]
        [Route("signup")]
        public IActionResult PostUsuario([FromBody] Usuario usuario)
        {

            try
            {
                               
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool existe = _usuarioBLL.EmailExiste(usuario.email);

                if (existe)
                {                    
                    return NotFound(new
                    {
                        message = "E-mail already exists",
                        errorCode = (int)HttpStatusCode.NonAuthoritativeInformation
                    });
                }


                _usuarioBLL.CadastrarUsuario(usuario, usuario.phones);
                             

                return Ok(new { message = "Usuário Cadastrado com sucesso", codigo = (int)HttpStatusCode.OK });

            }catch(Exception e)
            {

                return NotFound(new
                {
                    message = e.Message,
                    errorCode = (int)HttpStatusCode.NotFound
                });


            }
        }





      

        // GET: api/Usuario/5        
        [HttpPost]
        [Route("Signin")]
        public IActionResult Signin([FromBody]  LoginUsuario loginUsuario)
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
                return NotFound(new
                {
                    message = "Invalid e-mail or password",
                    errorCode = (int)HttpStatusCode.NotFound
                });
            }


            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);

            }
            catch (Exception e)
            {

                return NotFound(new
                {
                    message = e.Message,
                    errorCode = (int)HttpStatusCode.NotFound
                });


            }
        }
    }
}