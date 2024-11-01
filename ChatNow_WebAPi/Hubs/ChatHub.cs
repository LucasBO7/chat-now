using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Repositories;
using ChatNow_WebAPi.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace ChatNow_WebAPi.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        private readonly MessageRepository _messageRepository;

        public ChatHub()
        {
            _messageRepository = new();
        }

        public override async Task OnConnectedAsync()
        {
            await this.Clients.All.ReceiveMessage($"{Context.ConnectionId} entrou na aplicação!");
        }

        public async Task SendAsync(MessageRegisterDto insertedMessage)
        {
            // Registra a mensagem no banco de dados
            _messageRepository.Register(insertedMessage);

            // Envia a mensagem pro Evento 'ReceiveMessage'
            await Clients.Group(insertedMessage.ConversationId.ToString())
            //.ReceiveMessage($"{insertedMessage.SenderId}: {insertedMessage.Content} ({insertedMessage.ConversationId})");
            .ReceiveChatMessage(insertedMessage.SenderId.ToString(), insertedMessage.Content!, insertedMessage.ConversationId.ToString());
        }

        // Insere um Usuário em uma Conversa
        public async Task ConnectToGroup(string conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());

            // Retorna uma mensagem informando a inserção com sucesso! AO CLIENTE, NÃO SERVIDOR!!!
            await Clients.Caller.ReceiveMessage($"Você entrou no grupo {conversationId}");
        }

        //public override Task OnConnectedAsync()
        //{
        //    var userId = Context.User!.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

        //    if (userId == null)
        //        Console.WriteLine("Id do usuário não encontrado pelo token!");
        //    else
        //        Console.WriteLine("Usuário conectado com o Id: " + userId);
        //    return base.OnConnectedAsync();
        //}

        /// <summary>
        /// A chave única para cada grupo (ou identificador do grupo) será o Id da Conversa (IdConversation)
        /// </summary>
        /// <returns></returns>
        //public async Task AddToGroup(string conversationId)
        //{
        //    // Insere a conexão do usuário ao Grupo, 
        //    await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);
        //}

        /// <summary>
        /// Envia uma mensagem ao IdConversation desejado (Conversa)
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="message"></param>
        /// <param name="senderId"></param>
        /// <returns></returns>
        //public async Task SendMessageTo(string conversationId, string message, string senderId)
        //{
        //    await Clients.Group(conversationId).SendAsync("ReceiveMessage", message, senderId);
        //}

        //public async Task SendMessage(string user, string message)
        //{
        //    // Envia a mensagem a todos os clientes conectados ao Hub
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}


    }
}
