using ChatNow_WebAPi.Repositories;
using ChatNow_WebAPi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatNow_WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageRepository _messageRepository;

        public MessageController()
        {
            _messageRepository = new();
        }

        [HttpPost]
        public IActionResult Post(MessageRegisterDto insertedMessage)
        {
            try
            {
                _messageRepository.Register(insertedMessage);

                return Ok("Mensagem registrada com sucesso!");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [HttpGet("ChatMessages")]
        public async Task<IActionResult> Get(Guid idConversation)
        {
            try
            {
                var messagesListByTime = await _messageRepository.GetChatMessages(idConversation);

                if (messagesListByTime is null)
                    return BadRequest("Não foi encontrado nenhuma mensagem nesta conversa!");

                return Ok(messagesListByTime);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
