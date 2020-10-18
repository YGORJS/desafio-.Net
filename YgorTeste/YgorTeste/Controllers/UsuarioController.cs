using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YgorTeste.BLL;
using YgorTeste.Context;
using YgorTeste.DAL;
using YgorTeste.Models;

namespace YgorTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsuarioController(ApiContext context)
        {
            _context = context;
        }

        //// GET: api/Usuario
        //[HttpGet]
        //public IEnumerable<Usuario> GetUsuarios()
        //{
        //    return _context.Usuarios;
        //}

        //// GET: api/Usuario/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUsuario([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var usuario = await _context.Usuarios.FindAsync(id);

        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    MensagemUsuario msgusu = new MensagemUsuario();
        //    msgusu.Msg = "Usuário Cadastrado com sucesso";

        //    return Ok(msgusu);
        //}

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario([FromRoute] int id, [FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuario
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {

            try
            {

                UsuarioBLL usubll = new UsuarioBLL(new UsuarioDAL(_context));


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool existe = usubll.EmailExiste(usuario.email);

                if (existe)
                {                    
                    return NotFound(new
                    {
                        message = "E-mail already exists",
                        errorCode = (int)HttpStatusCode.NonAuthoritativeInformation
                    });
                }

                usuario.createdAt = DateTime.Now;
                _context.Usuarios.Add(usuario);


                foreach (Fone fones in usuario.fone)
                {
                    fones.usuarioid = usuario.Id;
                    _context.Fone.Add(fones);

                }

                await _context.SaveChangesAsync();
                             

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





        //// DELETE: api/Usuario/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUsuario([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var usuario = await _context.Usuarios.FindAsync(id);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Usuarios.Remove(usuario);
        //    await _context.SaveChangesAsync();

        //    return Ok(usuario);
        //}

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        // GET: api/Usuario/5        
        [HttpPost]
        [Route("Signin")]
        public async Task<IActionResult> Signin([FromRoute] string password, string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario =  _context.Usuarios.Where(a=>a.email.Equals(email) && a.password.Equals(password)).ToList() ;

            if (usuario == null)
            {
                return NotFound();
            }


            return CreatedAtAction("GetUsuario", new { id = usuario.FirstOrDefault().Id }, usuario.FirstOrDefault()); ;
        }
    }
}