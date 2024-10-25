using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Interfaces;
using ChatNow_WebAPi.Repositories;
using ChatNow_WebAPi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatNow_WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUserRepository _userRepository { get; set; }

        public LoginController()
        {
            _userRepository = new UserRepository();
        }

        /// <summary>
        /// Usa o JWT Bearer Authentication para Logar com Email em uma conta User existente no banco
        /// </summary>
        /// <param name="insertedUser">Objeto do tipo UserLoginEmailDto (Email, Password)</param>
        /// <returns>TokenJWT/NotFound</returns>
        [HttpPost]
        public IActionResult Post(UserLoginEmailDto insertedUser)
        {
            try
            {
                User searchedUser = _userRepository.SearchForEmailAndPassword(insertedUser.Email!, insertedUser.Password!)!;

                if (searchedUser is null)
                    return NotFound("Não foi encontrado nenhum usuário com o email e senha informados!");

                // Caso encontre o usuário buscado, prossegue para a criação do Token

                // 1 - Definir as informações(Claims) que serão fornecidas no Token (Payload)
                var claims = new[]
                {
                    // formato da claim(tipoDaClaim, valor)
                    new Claim(JwtRegisteredClaimNames.Jti, searchedUser.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, searchedUser.Email!),
                    // Existe a possibilidade de criar uma Claim personalizada
     	            //new Claim("Claim Personalizada", "Valor Personalizado")
                };

                // 2 - Definir a chave de acesso do toten
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chatnow-authentication-key-j321u432"));

                // 3 - Definir as credenciais do token (Header)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // 4 - Gerar o tokens
                var token = new JwtSecurityToken
                (
                    // emissor do token
                    issuer: "webapi.chatnow",

                    // destinatário
                    audience: "webapi.chatnow",

                    // dados definidos nas claims (Payload)
                    claims: claims,

                    // tempo de expiração
                    expires: DateTime.Now.AddMinutes(5),

                    // credenciais do token
                    signingCredentials: creds
                );

                // 5 - Retornar o token
                return Ok(new
                {
                    User = searchedUser,
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception error)
            {
                return BadRequest("Houve um erro: " + error);
            }
        }

        /// <summary>
        /// Usa JWT Bearer para logar-se em uma conta existente no banco com o google
        /// </summary>
        /// <param name="insertedUser"></param>
        /// <returns>TokenJWT/NotFound</returns>
        [HttpPost("LoginComGoogle")]
        public IActionResult? LoginWithGoogle(UserGoogleDto insertedUser)
        {
            try
            {
                User searchedUser = _userRepository.SearchByGoogleId(insertedUser.GoogleId!)!;

                if (searchedUser is null)
                    return NotFound("Usuário não encontrado!");

                // Caso encontre o usuário buscado, prossegue para a criação do Token

                // 1 - Definir as informações(Claims) que serão fornecidas no Token (Payload)
                var claims = new[]
                {
                    // formato da claim(tipoDaClaim, valor)
                    new Claim(JwtRegisteredClaimNames.Jti, searchedUser.Id.ToString()),
                    new Claim("Name", searchedUser.Name!),
                    new Claim("GoogleId", searchedUser.GoogleId!)
                    // Existe a possibilidade de criar uma Claim personalizada
     	            //new Claim("Claim Personalizada", "Valor Personalizado")
                };

                // 2 - Definir a chave de acesso do toten
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("chatnow-authentication-key-j321u432"));

                // 3 - Definir as credenciais do token (Header)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // 4 - Gerar o tokens
                var token = new JwtSecurityToken
                (
                    // emissor do token
                    issuer: "webapi.chatnow",

                    // destinatário
                    audience: "webapi.chatnow",

                    // dados definidos nas claims (Payload)
                    claims: claims,

                    // tempo de expiração
                    expires: DateTime.Now.AddMinutes(5),

                    // credenciais do token
                    signingCredentials: creds
                );

                // 5 - Retornar o token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}