using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatNow_WebAPi.Domains
{
    public class Conversation
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
    }
}
