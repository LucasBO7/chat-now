using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Infra;
using ChatNow_WebAPi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChatNow_WebAPi.Repositories
{
    public class ConversationRepository
    {
        private readonly Context _context;

        public ConversationRepository() => _context = new();


        public List<ConversationDto> GetMyConversations(Guid userId)
        {
            List<ConversationDto> formattedConversationList = new();
            // Busca uma Conversation por:
            // => usuário com Friendship
            // => Friendship com status 'Concluído'
            var conversationList = _context.Conversations
                .Include(c => c.Friendship)
                .Include(f => f.Friendship.UserOne)
                .Include(f => f.Friendship.UserTwo)
                .Where(c =>
                    (c.Friendship!.IdUserOne == userId || c.Friendship.IdUserTwo == userId) &&
                    c.Friendship.StatusId == Guid.Parse("4F122E07-7A16-47C4-BE2C-6671848C8417")).ToList();

            if (conversationList is null)
                return null!;

            foreach (var conversation in conversationList)
            {
                // Salva o UserTwo formatado
                if (conversation.Friendship!.IdUserOne == userId)
                {
                    formattedConversationList.Add(new()
                    {
                        IdConversation = conversation.Id,
                        IdFriend = conversation.Friendship.IdUserTwo,
                        IdFriendship = conversation.Friendship.IdFriendship,
                        FriendName = conversation.Friendship.UserTwo!.Name!,
                        FriendPhotoUrl = conversation.Friendship.UserTwo.PhotoUrl!
                    });
                }

                if (conversation.Friendship!.IdUserTwo == userId)
                {
                    formattedConversationList.Add(new()
                    {
                        IdConversation = conversation.Id,
                        IdFriend = conversation.Friendship.IdUserOne,
                        IdFriendship = conversation.Friendship.IdFriendship,
                        FriendName = conversation.Friendship.UserOne!.Name!,
                        FriendPhotoUrl = conversation.Friendship.UserOne.PhotoUrl!
                    });
                }
            }

            return formattedConversationList;
        }

    }
}