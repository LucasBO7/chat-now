using ChatNow_WebAPi.Domains;

namespace ChatNow_WebAPi.ViewModels
{
    public class ConversationDto
    {
        public Guid IdConversation { get; set; }
        public Guid IdFriendship { get; set; }
        public Guid IdFriend { get; set; }
        public string? FriendName { get; set; }
        public string? FriendPhotoUrl { get; set; }
    }
}