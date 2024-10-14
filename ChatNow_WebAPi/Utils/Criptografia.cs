namespace ChatNow_WebAPi.Utils
{
    public class Criptografia
    {
        /// <summary>
        /// Generate an Hash based on the inserted password
        /// </summary>
        /// <param name="password">Inserted password</param>
        /// <returns>hashed password</returns>
        public static string GenerateHash (string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifies if inserted password's hash is equals of db's hashed password
        /// </summary>
        /// <param name="insertedHashPassword">Password inserted by User</param>
        /// <param name="dbHashPassword">Password saved in db</param>
        /// <returns></returns>
        public static bool CompareHash (string insertedHashPassword, string dbHashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(insertedHashPassword, dbHashPassword);
        }
    }
}
