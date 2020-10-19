﻿using System;
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
        private readonly ApiContext _context;
        private readonly UsuarioDTO _usuarioDTO;
        private readonly IFoneBLL _foneBLL;
        private readonly IUsuarioBLL _usuarioBLL;

        public LoginUsuariosController(ApiContext context, UsuarioDTO usuarioDTO, IFoneBLL foneBLL, IUsuarioBLL usuarioBLL)
        {
            _context = context;
            _usuarioDTO = usuarioDTO;
            _foneBLL = foneBLL;
            _usuarioBLL = usuarioBLL;
        }

        // GET: api/LoginUsuarios
        [HttpGet]
        public IEnumerable<LoginUsuario> GetLoginUsuario()
        {
            return _context.LoginUsuario;
        }

        // GET: api/LoginUsuarios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoginUsuario([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginUsuario = await _context.LoginUsuario.FindAsync(id);

            if (loginUsuario == null)
            {
                return NotFound();
            }

            return Ok(loginUsuario);
        }

        // PUT: api/LoginUsuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginUsuario([FromRoute] int id, [FromBody] LoginUsuario loginUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loginUsuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(loginUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginUsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LoginUsuarios
        [HttpPost]
        [Route("Signin")]
        public async Task<IActionResult> PostLoginUsuario([FromBody] LoginUsuario loginUsuario)
        {

            try
            {
          
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var usuario = _usuarioBLL.ObterUsuario(loginUsuario.Email, loginUsuario.password);

                if (usuario == null)
                {                    
                    return NotFound(new { message = "Invalid e-mail or password", errorcode = (int)HttpStatusCode.NotFound });
                }
                
                var fones = _foneBLL.OterFonesUsuario(usuario.Id);

                usuario.fone.Clear();

                foreach (Fone foneusu in fones)
                {
                    usuario.fone.Add(foneusu);
                }


                _usuarioBLL.AtualizarUsuario(usuario);

                _usuarioDTO.firstName = usuario.firstName;
                _usuarioDTO.lastName = usuario.lastName;
                _usuarioDTO.email = usuario.email;
                _usuarioDTO.fone = usuario.fone;
                _usuarioDTO.created_at = usuario.createdAt.ToString("MM/dd/yyyy HH:mm");
                _usuarioDTO.last_login = usuario.last_login.ToString("MM/dd/yyyy HH:mm");


                return Ok(new { message = "Login realizado com sucesso" , codigo= (int)HttpStatusCode.OK, usuario = _usuarioDTO});

            }catch(Exception e)
            {                
                return NotFound( new { message = e.Message , errorCode = (int)HttpStatusCode.NotFound });
            }


        }

        // POST: api/LoginUsuarios
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("me")]
        public async Task<IActionResult> PostLoginUsuarioToken([FromBody] LoginUsuario loginUsuario)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var usuario = _usuarioBLL.ObterUsuario(loginUsuario.Email, loginUsuario.password);

                if (usuario == null)
                {                   
                    return NotFound(new { message = "Invalid e-mail or password" , errorcode= (int)HttpStatusCode.NotFound });
                }
                var fones = _foneBLL.OterFonesUsuario(usuario.Id);

                usuario.fone.Clear();

                foreach (Fone foneusu in fones)
                {
                    usuario.fone.Add(foneusu);
                }


                _usuarioBLL.AtualizarUsuario(usuario);

                _usuarioDTO.firstName = usuario.firstName;
                _usuarioDTO.lastName = usuario.lastName;
                _usuarioDTO.email = usuario.email;
                _usuarioDTO.fone = usuario.fone;
                _usuarioDTO.created_at = usuario.createdAt.ToString("MM/dd/yyyy HH:mm");
                _usuarioDTO.last_login = usuario.last_login.ToString("MM/dd/yyyy HH:mm");

                return Ok(new { usuario = _usuarioDTO });

            }
            catch (Exception e)
            {               
                return NotFound(new {message=e.Message, errorcode= (int)HttpStatusCode.NotFound });
            }


        }

        // DELETE: api/LoginUsuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoginUsuario([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginUsuario = await _context.LoginUsuario.FindAsync(id);
            if (loginUsuario == null)
            {
                return NotFound();
            }

            _context.LoginUsuario.Remove(loginUsuario);
            await _context.SaveChangesAsync();

            return Ok(loginUsuario);
        }

        private bool LoginUsuarioExists(int id)
        {
            return _context.LoginUsuario.Any(e => e.Id == id);
        }
    }
}