using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Interfaces;
using ChatNow_WebAPi.Repositories;
using ChatNow_WebAPi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatNow_WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserRepository _userRepository { get; set; }

        public UserController() => _userRepository = new UserRepository();

        /// <summary>
        /// Cadastra um novo User no Banco de Dados
        /// </summary>
        /// <param name="insertedUser">Objeto do tipo User</param>
        /// <returns>OK(Objeto User) or BADREQUEST</returns>
        [HttpPost]
        public IActionResult Post(User insertedUser)
        {
            try
            {
                var result = _userRepository.Register(insertedUser);

                if (result is null)
                    return BadRequest("O usuário já está registrado!");
                else
                    return Ok("Usuário cadastrado com sucesso!");
            }
            catch (Exception error)
            {
                return BadRequest("Houve um erro: " + error);
            }
        }

        /// <summary>
        /// Cadastra um novo User pelo GoogleId dele no Banco de Dados
        /// </summary>
        /// <param name="insertedUser">Objeto do tipo UserGoogleDto(Email, GoogleId)</param>
        /// <returns>OK/BadRequest</returns>

        [HttpPost("CadastroComGoogle")]
        public IActionResult PostWithGoogle(UserGoogleDto insertedUser)
        {
            try
            {
                var result = _userRepository.RegisterWithGoogle(insertedUser);

                if (result is null)
                    return BadRequest("O usuário inserido já existe!");

                return Ok("Usuário cadastrado com sucesso!");
            }
            catch (Exception error)
            {
                return BadRequest("Houve um erro: " + error);
            }
        }


        /// <summary>
        /// Lista todos os usuários salvos no Banco de Dados
        /// </summary>
        /// <returns>Lista de Users/BadRequest</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var usersList = _userRepository.ListAll();

                return usersList is null
                    ? BadRequest("Não há nenhum usuário!")
                    : Ok(usersList);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

    }
}
