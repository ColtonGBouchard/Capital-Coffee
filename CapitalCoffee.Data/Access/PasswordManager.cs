using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCoffee.Data.Access
{
    public class PasswordManager
    {
        HashComputer hashComputer = new HashComputer();

        public string GeneratePasswordHash(string plainTextPassword, string salt)
        {
            string finalString = plainTextPassword + salt;
            return hashComputer.GetPasswordHashAndSalt(finalString);
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            string finalString = password + salt;
            var hashedResult = hashComputer.GetPasswordHashAndSalt(finalString);
            return hash == hashedResult;
        }
    }
}
