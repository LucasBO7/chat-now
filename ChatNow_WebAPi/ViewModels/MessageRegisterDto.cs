using System.ComponentModel.DataAnnotations;

namespace ChatNow_WebAPi.ViewModels
{
    public class MessageRegisterDto
    {
        [Required(ErrorMessage = "O Id da Conversa(Conversation) é obrigatória!")]
        public Guid ConversationId { get; set; }


        [Required(ErrorMessage = "O Id do usuário que enviou a mensagem é obrigatório!")]
        public Guid SenderId { get; set; }


        [Required(ErrorMessage = "A propriedade Content é obrigatória!")]
        public string? Content { get; set; }
    }
}
