using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YgorTeste.BLL;
using YgorTeste.Context;
using YgorTeste.DAL;
using YgorTeste.Mensagem;
using YgorTeste.Models;

namespace YgorTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginUsuariosController : ControllerBase
    {
        private readonly ApiContext _context;

        public LoginUsuariosController(ApiContext context)
        {
            _context = context;
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

                UsuarioBLL usuBll = new UsuarioBLL(new UsuarioDAL(_context));
                FoneBLL foneBll = new FoneBLL(new FoneDAL(_context));

                var usuario = usuBll.ObterUsuario(loginUsuario.Email, loginUsuario.password);

                if (usuario == null)
                {
                    MensagemUsuario msg = new MensagemUsuario();
                    msg.Msg = "Invalid e-mail or password";
                    msg.Codigo = (int)HttpStatusCode.NotFound;
                    return Ok(msg);
                }
                var fones = foneBll.OterFonesUsuario(usuario.Id);

                usuario.fone.Clear();

                foreach (Fone foneusu in fones)
                {
                    usuario.fone.Add(foneusu);
                }


                usuBll.AtualizarUsuario(usuario);

                MensagemUsuarioObjeto msgusu = new MensagemUsuarioObjeto();
                msgusu.Msg = "Login realizado com sucesso";
                msgusu.Codigo = (int)HttpStatusCode.OK;
                msgusu.usuario = usuario;

                return Ok(msgusu);

            }catch(Exception e)
            {
                MensagemUsuario msgerro = new MensagemUsuario();
                msgerro.Msg = e.Message;
                msgerro.Codigo = (int) HttpStatusCode.NotFound; 
                return Ok(msgerro);
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

                UsuarioBLL usuBll = new UsuarioBLL(new UsuarioDAL(_context));
                FoneBLL foneBll = new FoneBLL(new FoneDAL(_context));

                var usuario = usuBll.ObterUsuario(loginUsuario.Email, loginUsuario.password);

                if (usuario == null)
                {
                    MensagemUsuario msg = new MensagemUsuario();
                    msg.Msg = "Invalid e-mail or password";
                    msg.Codigo = (int)HttpStatusCode.NotFound;
                    return Ok(msg);
                }
                var fones = foneBll.OterFonesUsuario(usuario.Id);

                usuario.fone.Clear();

                foreach (Fone foneusu in fones)
                {
                    usuario.fone.Add(foneusu);
                }


                usuBll.AtualizarUsuario(usuario);

                MensagemUsuarioObjeto msgusu = new MensagemUsuarioObjeto();
                msgusu.Msg = "Login realizado com sucesso";
                msgusu.Codigo = (int)HttpStatusCode.OK;
                msgusu.usuario = usuario;

                return Ok(msgusu);

            }
            catch (Exception e)
            {
                MensagemUsuario msgerro = new MensagemUsuario();
                msgerro.Msg = e.Message;
                msgerro.Codigo = (int)HttpStatusCode.NotFound;
                return Ok(msgerro);
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