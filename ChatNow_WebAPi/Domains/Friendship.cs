using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChatNow_WebAPi.Domains
{
    public class Friendship
    {
        [Key]
        public Guid IdFriendship { get; set; }

        [ForeignKey("IdUserOne")]
        [Required(ErrorMessage = "O id do usuário primário é obrigatório!")]
        public Guid IdUserOne { get; set; }
        public User? UserOne { get; set; }


        [ForeignKey("IdUserTwo")]
        [Required(ErrorMessage = "O id do usuário secundário é obrigatório!")]
        public Guid IdUserTwo { get; set; }
        public User? UserTwo { get; set; }


        [ForeignKey("StatusId")]
        [Required(ErrorMessage = "O id do usuário secundário é obrigatório!")]
        public Guid StatusId { get; set; }
        public Status Status { get; set; }
    }
}
