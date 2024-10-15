using ChatNow_WebAPi.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatNow_WebAPi.ViewModels
{
    public class ShowMessageDto
    {
        public Guid Id { get; set; }

        public Guid ConversationId { get; set; }

        public Guid SenderId { get; set; }

        public string? Content { get; set; }

        public DateTime SentTime { get; set; }
    }
}
