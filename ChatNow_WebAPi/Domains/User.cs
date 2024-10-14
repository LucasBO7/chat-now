using System.ComponentModel.DataAnnotations;

namespace ChatNow_WebAPi.Domains
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [StringLength(24)]
        [Required(ErrorMessage = "O nome de usuário é obrigatório!")]
        public string? Name { get; set; }

        [StringLength(256)]
        public string? Email { get; set; }

        [StringLength(256)]
        public string? Password { get; set; }

        [StringLength(256)]
        public string? GoogleId { get; set; }
    }
}