using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatNow_WebAPi.Domains
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(Conversation))]
        [Required(ErrorMessage = "O Id da Conversa(Conversation) é obrigatória!")]
        public Guid ConversationId { get; set; }
        public Conversation? Conversation { get; set; }


        [ForeignKey(nameof(User))]
        [Required(ErrorMessage = "O Id do usuário que enviou a mensagem é obrigatório!")]
        public Guid SenderId { get; set; }
        public User? Sender { get; set; }


        [StringLength(400)]
        [Required(ErrorMessage = "A propriedade Content é obrigatória!")]
        public string? Content { get; set; }

        [Required(ErrorMessage = "O Horário do envio da mensagem é obrigatório!")]
        public DateTime SentTime { get; private set; } = DateTime.UtcNow;
    }
}
