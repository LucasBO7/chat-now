using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatNow_WebAPi.Domains
{
    public class UserConversation
    {
        [Key]
        public Guid IdUserConversation { get; set; }


        [ForeignKey("IdUser")]
        [Required(ErrorMessage = "O id do usuário primário é obrigatório!")]
        public Guid IdUser { get; set; }
        public User? User { get; set; }


        [ForeignKey("IdConversation")]
        [Required(ErrorMessage = "O id do usuário primário é obrigatório!")]
        public Guid IdConversation { get; set; }
        public Conversation? Conversation { get; set; }
    }
}
