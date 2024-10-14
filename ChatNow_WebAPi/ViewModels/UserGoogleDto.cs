using System.ComponentModel.DataAnnotations;

namespace ChatNow_WebAPi.ViewModels
{
    public class UserGoogleDto
    {
        [StringLength(24)]
        [Required(ErrorMessage = "O nome de usuário é obrigatório!")]
        public string? Name { get; set; }

        [StringLength(256)]
        [Required(ErrorMessage = "O Id de autenticação do Google é obrigatório!")]
        public string? GoogleId { get; set; }
    }
}
