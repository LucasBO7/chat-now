using ChatNow_WebAPi.Domains;
using ChatNow_WebAPi.ViewModels;

namespace ChatNow_WebAPi.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Registers a new User in Db
        /// </summary>
        /// <param name="newUser">Object tyoe User</param>
        /// <returns>NULL or Object User</returns>
        public User? Register(User newUser);

        /// <summary>
        /// Registers a new User in Db with Google Login
        /// </summary>
        /// <param name="newUser">Object User</param>
        /// <returns>NULL or Object User</returns>
        public User? RegisterWithGoogle(UserGoogleDto newUser);

        /// <summary>
        /// Searches an User in Db
        /// </summary>
        /// <param name="email">email inserted</param>
        /// <param name="password">password inserted</param>
        /// <returns>NULL or Object User</returns>
        public User? SearchForEmailAndPassword(string email, string password);

        /// <summary>
        /// Searches an User with Google in Db
        /// </summary>
        /// <param name="googleId">GoogleId inserted</param>
        /// <returns>NULL or Object User</returns>
        public User? SearchByGoogleId(string googleId);
    }
}
