using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Interfaces;
using ChatNow_WebAPi.Repositories;
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
        /// Registers a new User in Db
        /// </summary>
        /// <param name="insertedUser">Object of User type</param>
        /// <returns>OK or BADREQUEST</returns>
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
                return BadRequest(error.Message);
            }
        }
    }
}
