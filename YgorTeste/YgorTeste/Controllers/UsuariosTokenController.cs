using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using YgorTeste.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using YgorTeste.IService;
using System.Net;

namespace YgorTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosTokenController : ControllerBase
    {
        private readonly UserManager<ApplicationUserToken> _userManager;
        private readonly SignInManager<ApplicationUserToken> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public UsuariosTokenController(UserManager<ApplicationUserToken> userManager,
            SignInManager<ApplicationUserToken> signInManager,
            IConfiguration configuration, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenService = tokenService;

        }
        [HttpGet]
        public ActionResult<string> Get()
        {
            return " << Controlador UsuariosController :: WebApiUsuarios >> ";
        }
        [HttpPost("Criar")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfoToken model)
        {
            try {
                var user = new ApplicationUserToken { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return _tokenService.BuildToken(model);
                }
                else
                {
                    return BadRequest("Usuário ou senha inválidos");
                }
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
        [HttpPost("Obter")]
        public async Task<ActionResult<UserToken>> Obter([FromBody] UserInfoToken userInfo)
        {
            try{ 
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password,
                     isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return _tokenService.BuildToken(userInfo);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "login inválido.");
                    return BadRequest(ModelState);
                }
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