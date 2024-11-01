using ChatNow_WebAPi.Hubs;
using ChatNow_WebAPi.Repositories;
using ChatNow_WebAPi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatNow_WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageRepository _messageRepository;

        //private IHubContext<ChatHub, IChatHub> _hub;

        //public MessageController(IHubContext<ChatHub, IChatHub> hub)
        //{
        //    _messageRepository = new MessageRepository(hub);
        //    _hub = hub;
        //}

        public MessageController()
        {
            _messageRepository = new();
        }

        //[HttpPost("Enviar")]
        //public async Task<IActionResult> Post(string message)
        //{
        //    try
        //    {
        //        await _hub.Clients.All.ReceiveMessage(message);

        //        return Ok("Mensagem enviada com sucesso!");
        //    }
        //    catch (Exception error)
        //    {
        //        return BadRequest(error.Message);
        //    }
        //}

        //[HttpPost("EntrarNoGrupo")]
        //public async Task<IActionResult> PostAddGroup(Guid conversationId)
        //{
        //    try
        //    {
        //        ChatHub hub = new();

        //        await hub.AddToGroup(conversationId.ToString());

        //        return Ok("Entrado no grupo com sucesso!");
        //    }
        //    catch (Exception error)
        //    {
        //        return BadRequest(error.Message);
        //    }
        //}

        //[HttpPost("EnviarParaAll")]
        //public async Task<IActionResult> PostForAll(Guid conversationId)
        //{
        //    try
        //    {
        //        ChatHub hub = new();

        //        await hub.SendMessage("Banana", "Mensagem está aqui eu estou falando sim!");

        //        return Ok("Mensagem enviada com sucesso!");
        //    }
        //    catch (Exception error)
        //    {
        //        return BadRequest(error.Message);
        //    }
        //}


        [HttpPost]
        public IActionResult Post(MessageRegisterDto insertedMessage)
        {
            try
            {
                _messageRepository.Register(insertedMessage);

                return Ok("Mensagem enviada com sucesso!");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [HttpGet("BuscarMensagensConversa")]
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
