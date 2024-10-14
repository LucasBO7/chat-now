using ChatNow_WebAPi.Domains;

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
        /// Searches an User in Db
        /// </summary>
        /// <param name="email">email inserted</param>
        /// <param name="password">password inserted</param>
        /// <returns>Object User</returns>
        public User? SearchForEmailAndPassword(string email, string password);
    }
}
