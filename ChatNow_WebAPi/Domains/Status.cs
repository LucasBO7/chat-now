using System.ComponentModel.DataAnnotations;

namespace ChatNow_WebAPi.Domains
{
    public class Status
    {
        [Key]
        public Guid IdStatus { get; set; }

        [Required]
        [StringLength(30)]
        public string? Text { get; set; }
    }
}
