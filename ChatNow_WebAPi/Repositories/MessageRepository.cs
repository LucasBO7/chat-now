using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Infra;
using ChatNow_WebAPi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChatNow_WebAPi.Repositories
{
    public class MessageRepository
    {
        private readonly Context _context;

        public MessageRepository()
        {
            _context = new();
        }

        public void Register(MessageRegisterDto insertedMessage)
        {
            Message newMessage = new()
            {
                Content = insertedMessage.Content,
                SenderId = insertedMessage.SenderId,
                ConversationId = insertedMessage.ConversationId,
            };

            _context.Messages.Add(newMessage);
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
