using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.Infra;
using ChatNow_WebAPi.Interfaces;
using ChatNow_WebAPi.Utils;
using ChatNow_WebAPi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ChatNow_WebAPi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository()
        {
            _context = new();
        }

        /// <summary>
        /// Registers a new User in Db with Email or GoogleId
        /// </summary>
        /// <param name="newUser">Object of type User</param>
        /// <returns>NULL or User Object</returns>
        public User? Register(User newUser)
        {
            #region Validação - Se existe - Se tem valor
            // Verifica se há algum Usuário com Email ou GoogleId igual
            bool userAlreadyRegistered = _context.Users.Any(u => u.Email == newUser.Email || u.GoogleId == newUser.GoogleId);

            // Se o usuário já estiver registrado, ou não tiver inserido o email ou GoogleId, retorna null
            if (userAlreadyRegistered || (String.IsNullOrEmpty(newUser.Email) && String.IsNullOrEmpty(newUser.Password)) && String.IsNullOrEmpty(newUser.GoogleId))
            {
                return null;
            }
            #endregion


            // Gera e salva senha criptografada
            newUser.Password = Criptografia.GenerateHash(newUser.Password!);

            // Insere o novo usuário
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }
        public User? RegisterWithGoogle(UserGoogleDto insertedUser)
        {
            bool userAlreadyExists = _context.Users.Any(u => u.GoogleId == insertedUser.GoogleId);

            // Se já existir ou googleId estiver vazio
            if (userAlreadyExists || String.IsNullOrEmpty(insertedUser.GoogleId))
                return null;

            // Formata para o usuário completo
            User newUser = new()
            {
                Name = insertedUser.Name,
                GoogleId = Criptografia.GenerateHash(insertedUser.GoogleId)
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        /// <summary>
        /// Searches an existent user by Email and Password
        /// </summary>
        /// <param name="email">Email inserted</param>
        /// <param name="password">Password inserted</param>
        /// <returns>NULL or User Object</returns>
        public User? SearchForEmailAndPassword(string email, string password)
        {
            // Busca por email e senha
            User? userSearched = _context.Users.FirstOrDefault(u => u.Email == email)!;

            // Se o usuário não for encontrado
            if (userSearched is null)
                return null;

            // Verifica se a senha criptografada é igual
            bool isPasswordEqual = Criptografia.CompareHash(password, userSearched.Password!);

            // Se a SENHA e EMAIL forem iguais
            if (isPasswordEqual)
            {
                return userSearched;
            }

            // Se a senha NÃO for igual
            return null;
        }


        /// <summary>
        /// Searches an User by his GoogleId
        /// </summary>
        /// <param name="googleId">GoogleId inserted</param>
        /// <returns>NULL or User Object</returns>
        public User SearchByGoogleId(string googleId)
        {
            var usersLoggedWithGoogle = _context.Users.Where(u => u.GoogleId != null).ToList();

            var searchedUser = usersLoggedWithGoogle.FirstOrDefault(u => Criptografia.CompareHash(googleId, u.GoogleId!) == true);

            if (searchedUser is null)
                return null!;

            return searchedUser;

            //var usersLoggedWithGoogle = _context.Users.Where(u => u.GoogleId != null).ToList();

            //foreach (var item in usersLoggedWithGoogle)
            //{
            //    bool isEqual = Criptografia.CompareHash(googleId, item.GoogleId!);

            //    if (isEqual)
            //        return item;
            //}

            //return null;
        }

    }
}