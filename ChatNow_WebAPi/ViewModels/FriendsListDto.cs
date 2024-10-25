using ChatNow_WebAPi.Domains;
using System.ComponentModel.DataAnnotations;


namespace ChatNow_WebAPi.ViewModels
{
    public class FriendsListDto
    {
        public Guid IdFriendship { get; set; }


        public Guid IdUser { get; set; } = new Guid();
        public string? Name { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }


        public string? GoogleId { get; set; }
        public Status Status { get; set; }
    }
}