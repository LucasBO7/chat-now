using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Infra;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System;
using ChatNow_WebAPi.ViewModels;

namespace ChatNow_WebAPi.Repositories
{
    public class FriendshipRepository
    {
        private readonly Context _context;

        public FriendshipRepository()
        {
            _context = new();
        }

        public Friendship AcceptFriendInvite(Guid idFriendship)
        {
            Friendship searchedFriendship = _context.Friendships.FirstOrDefault(f => f.IdFriendship == idFriendship)!;

            if (searchedFriendship is null)
                return null;

            // Altera o id do status "Confirmado"
            searchedFriendship.StatusId = Guid.Parse("4F122E07-7A16-47C4-BE2C-6671848C8417");

            // Atualiza a Friendship
            _context.Friendships.Update(searchedFriendship);

            // Salva o banco de dados
            _context.SaveChanges();

            // retorna o objeto atualizado
            return searchedFriendship;
        }


        public async void AddFriend(Guid idActualUser, Guid idPendentUser)
        {
            // Cria nova Friendship
            Friendship newFriendship = new()
            {
                IdUserOne = idActualUser,
                IdUserTwo = idPendentUser,
                StatusId = Guid.Parse("C653E1B0-DBC2-417D-AEE6-32EE4F2E0C01"),
            };

            // Filtra as Friendships para verificar se existe uma com os mesmos usuário
            var friendshipAlredyExists = _context.Friendships.Any(f =>
            (f.IdUserOne == idActualUser || f.IdUserOne == idActualUser)
            &&
            (f.IdUserTwo == idPendentUser || f.IdUserTwo == idPendentUser));

            // Adiciona na tabela
            if (!friendshipAlredyExists)
                _context.Friendships.Add(newFriendship);

            // Cria uma nova Conversa com o Id da Friendship criada
            Guid newConversationId = await CreateConversation(newFriendship.IdFriendship);

            // Cria uma referência do Usuário atual e do Remetente à Conversa 
            //CreateUserConversations([idActualUser, idPendentUser], newConversationId);

            // Salva as alterações
            _context.SaveChanges();
        }

        private async Task<Guid> CreateConversation(Guid idFriendship)
        {
            // Cria uma nova conversa
            var newConversation = new Conversation
            {
                IdFriendship = idFriendship
            };

            // Adiciona a nova conversa ao contexto
            await _context.Conversations.AddAsync(newConversation);

            // Salva as alterações no banco de dados para obter o ID da nova conversa
            await _context.SaveChangesAsync();

            return newConversation.Id;
        }
        //private void CreateUserConversations(Guid[] userIds, Guid idConversation)
        //{
        //    foreach (var userId in userIds)
        //    {
        //        var newUserConversation = new UserConversation()
        //        {
        //            IdUser = userId,
        //            IdConversation = idConversation
        //        };

        //        _context.UserConversation.Add(newUserConversation);
        //    }
        //}


        public void RemoveFriend(Guid idFriendship)
        {
            var friendshipToDelete = _context.Friendships.FirstOrDefault(f => f.IdFriendship == idFriendship);
            var conversationToDelete = _context.Conversations.FirstOrDefault(c => c.IdFriendship == idFriendship);

            if (friendshipToDelete is not null && conversationToDelete is not null)
            {

                // Continuar lógica de remoção da Conversa
                //_context.UserConversation.FirstOrDefault(uc => uc.IdUser == friendshipToDelete.IdUserOne || uc.IdUser == friendshipToDelete.IdUserTwo);

                _context.Friendships.Remove(friendshipToDelete);
                _context.Conversations.Remove(conversationToDelete);
            }

            _context.SaveChanges();
        }

        public List<FriendsListDto>? ListAllFriends(Guid idUser)
        {
            List<Friendship> friendsList = _context.Friendships
                .Where(f => f.IdUserOne == idUser || f.IdUserTwo == idUser)
                .Include(f => f.UserOne)
                .Include(f => f.UserTwo)
                .Include(f => f.Status)
                .ToList();

            List<FriendsListDto> formattedFriendsList = new();

            foreach (var friendship in friendsList)
            {
                // Se o Usuário que pesquisou for UserOne
                if (friendship.IdUserOne == idUser)
                {
                    // Salva o UserTwo formatado
                    formattedFriendsList.Add(new FriendsListDto
                    {
                        IdFriendship = friendship.IdFriendship,
                        Status = friendship.Status,
                        // Dados do usuário
                        IdUser = friendship.UserTwo!.Id,
                        Name = friendship.UserTwo.Name,
                        Email = friendship.UserTwo.Email,
                        Password = friendship.UserTwo.Password,
                        GoogleId = friendship.UserTwo.GoogleId,
                        PhotoUrl = friendship.UserTwo.PhotoUrl
                    });
                }

                // Se o usuário que pesquisou for UserTwo
                if (friendship.IdUserTwo == idUser)
                {
                    // Salva o UserOne formatado
                    formattedFriendsList.Add(new FriendsListDto
                    {
                        IdFriendship = friendship.IdFriendship,
                        Status = friendship.Status,
                        // Dados do usuário
                        IdUser = friendship.UserOne!.Id,
                        Name = friendship.UserOne.Name,
                        Email = friendship.UserOne.Email,
                        Password = friendship.UserOne.Password,
                        GoogleId = friendship.UserOne.GoogleId,
                        PhotoUrl = friendship.UserOne.PhotoUrl
                    });
                }
            }

            return formattedFriendsList.Count > 0 ? formattedFriendsList : null; // Retorna null se a lista estiver vazia
        }
    }
}
