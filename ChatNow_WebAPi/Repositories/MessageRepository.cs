using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Hubs;
using ChatNow_WebAPi.Infra;
using ChatNow_WebAPi.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatNow_WebAPi.Repositories
{
    public class MessageRepository
    {
        private readonly Context _context;
        //private readonly IHubContext<ChatHub, IChatHub> _hub;

        public MessageRepository()
        {
            _context = new();
            //_hub = hub;
        }

        public async void Register(MessageRegisterDto insertedMessage)
        {
            Message newMessage = new()
            {
                Content = insertedMessage.Content,
                SenderId = insertedMessage.SenderId,
                ConversationId = insertedMessage.ConversationId,
            };

            _context.Messages.Add(newMessage);



            // Envia a mensagem ao usuário com SignalR

            //await _hub.Groups.AddToGroupAsync(insertedMessage.SenderId.ToString(), insertedMessage.ConversationId.ToString());
            //_hub.Groups.AddToGroupAsync()

            //await _hub.Groups.AddToGroupAsync(Context.ConnectionId, insertedMessage.ConversationId);
            //_hub.Clients.Group(insertedMessage.ConversationId.ToString()).ReceiveMessage();

            //await chatHub.SendMessageTo(Convert.ToString(insertedMessage.ConversationId)!, insertedMessage.Content!, Convert.ToString(insertedMessage.SenderId)!);

            _context.SaveChanges();
        }

        public async Task<List<ShowMessageDto>?> GetChatMessages(Guid idConversation)
        {
            List<Message> messagesList = await _context.Messages
                .Where(m => m.ConversationId == idConversation)
                .OrderBy(m => m.SentTime)
                .ToListAsync();

            List<ShowMessageDto> formattedMessagesList = new();
            messagesList.ForEach(m => formattedMessagesList.Add(new ShowMessageDto
            {
                Id = m.Id,
                ConversationId = m.ConversationId,
                SenderId = m.SenderId,
                Content = m.Content,
                SentTime = m.SentTime
            }));

            return formattedMessagesList.Any() ? formattedMessagesList : null;
        }
    }
}
