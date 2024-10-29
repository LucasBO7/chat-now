using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatNow_WebAPi.Domains
{
    public class Conversation
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required(ErrorMessage = "O Id da Friendship é obrigatório!")]
        public Guid IdFriendship { get; set; }
        [ForeignKey("IdFriendship")]
        public Friendship? Friendship { get; set; }
    }
}
