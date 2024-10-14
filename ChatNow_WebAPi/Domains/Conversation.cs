using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatNow_WebAPi.Domains
{
    public class Conversation
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [ForeignKey("UserOneId")]
        [Required(ErrorMessage = "O id do usuário primário é obrigatório!")]
        public Guid UserId { get; set; }
        public User? UserOne { get; set; }


        [ForeignKey("UserTwoId")]
        [Required(ErrorMessage = "O id do usuário secundário é obrigatório!")]
        public Guid UserTwoId { get; set; }
        public User? UserTwo { get; set; }
    }
}
