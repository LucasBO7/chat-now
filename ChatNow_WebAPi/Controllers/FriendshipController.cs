using ChatNow_WebAPi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatNow_WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly FriendshipRepository _friendshipRepository;

        public FriendshipController()
        {
            _friendshipRepository = new();
        }

        [HttpPost]
        public IActionResult Post(Guid actualUser, Guid receiverUser)
        {
            try
            {
                _friendshipRepository.AddFriend(actualUser, receiverUser);
                return Ok("Novo amigo adicionado com sucesso!");    
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete("RemoverAmigo")]
        public IActionResult Delete(Guid idFriendship)
        {
            try
            {
                _friendshipRepository.RemoveFriend(idFriendship);
                return Ok("Removido amigo da lista de amizades!");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("Amigos")]
        public IActionResult GetAllFriend(Guid idUser)
        {
            try
            {
                var friendsList = _friendshipRepository.ListAllFriends(idUser);
                if (friendsList is null)
                    return NotFound("Nenhum amigo encontrado!");

                return Ok(friendsList);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
