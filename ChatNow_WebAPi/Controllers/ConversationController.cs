using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Repositories;
using ChatNow_WebAPi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatNow_WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly ConversationRepository _conversationRepository;

        public ConversationController() => _conversationRepository = new();

        [HttpGet("BuscarConversas")]
        public IActionResult Get(Guid userId)
        {
            try
            {
                List<ConversationDto> conversations = _conversationRepository.GetMyConversations(userId);

                if (conversations is null)
                    return NotFound("Nenhuma conversa encontrada, verifique se você possui amigos!");

                return Ok(conversations);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

    }
}
