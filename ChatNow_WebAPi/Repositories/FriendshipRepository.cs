using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Infra;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System;

namespace ChatNow_WebAPi.Repositories
{
    public class FriendshipRepository
    {
        private readonly Context _context;

        public FriendshipRepository()
        {
            _context = new();
        }


        public async void AddFriend(Guid idActualUser, Guid idPendentUser)
        {
            // Cria nova Friendship
            Friendship newFriendship = new()
            {
                IdUserOne = idActualUser,
                IdUserTwo = idPendentUser,
                StatusId = Guid.Parse("14A343A9-A6AC-4845-BB24-D5561BA7983D"),
            };

            // Filtra as Friendships para verificar se existe uma com os mesmos usuário
            var friendshipAlredyExists = _context.Friendships.Any(f =>
            (f.IdUserOne == idActualUser || f.IdUserOne == idActualUser)
            &&
            (f.IdUserTwo == idPendentUser || f.IdUserTwo == idPendentUser));

            // Adiciona na tabela
            if (!friendshipAlredyExists)
                _context.Friendships.Add(newFriendship);

            // Cria uma nova Conversa
            Guid newConversationId = await CreateConversation();

            // Cria uma referência do Usuário atual e do Remetente à Conversa 
            CreateUserConversations([idActualUser, idPendentUser], newConversationId);

            // Salva as alterações
            _context.SaveChanges();
        }

        private async Task<Guid> CreateConversation()
        {
            // Cria uma nova conversa
            var newConversation = new Conversation();

            // Adiciona a nova conversa ao contexto
            await _context.Conversations.AddAsync(newConversation);

            // Salva as alterações no banco de dados para obter o ID da nova conversa
            await _context.SaveChangesAsync();

            return newConversation.Id;
        }
        private void CreateUserConversations(Guid[] userIds, Guid idConversation)
        {
            foreach (var userId in userIds)
            {
                var newUserConversation = new UserConversation()
                {
                    IdUser = userId,
                    IdConversation = idConversation
                };

                _context.UserConversation.Add(newUserConversation);
            }
        }


        public void RemoveFriend(Guid idFriendship)
        {
            var friendshipToDelete = _context.Friendships.FirstOrDefault(f => f.IdFriendship == idFriendship);

            if (friendshipToDelete is not null)
                _context.Friendships.Remove(friendshipToDelete);

            _context.SaveChanges();
        }

        public List<Friendship>? ListAllFriends(Guid idUser)
        {
            List<Friendship> friendsList = _context.Friendships
                .Where(f => f.IdUserOne == idUser || f.IdUserTwo == idUser)
                .Include(f => f.UserOne)
                .Include(f => f.UserTwo)
                .ToList();

            foreach (var friendship in friendsList)
            {
                if (friendship.IdUserOne == idUser)
                    friendship.UserOne = null;
                if (friendship.IdUserTwo == idUser)
                    friendship.UserTwo = null;
            }

            return friendsList.Count > 0 ? friendsList : null; // Retorna null se a lista estiver vazia
        }
    }
}
