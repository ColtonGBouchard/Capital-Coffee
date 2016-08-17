using CapitalCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalCoffee.Data.Access
{
    public class UserDao
    {
        private readonly CapitalCoffeeContext context;

        public UserDao(CapitalCoffeeContext context)
        {
            this.context = context;
        }

        public void RegisterAccount(User user, string password)
        {
            HashComputer hashComputer = new HashComputer();
            PasswordManager passwordManager = new PasswordManager();
            string salt = SaltGenerator.GetSaltString();
            var hash = passwordManager.GeneratePasswordHash(password, salt);

            user.PasswordHash = hash;
            user.Salt = salt;
            user.RoleId = 2;

            context.Users.Add(user);
            context.SaveChanges();
        }

        public bool CheckLogin(string input, string password)
        {
            HashComputer hashComputer = new HashComputer();
            PasswordManager pm = new PasswordManager();
            User user = context.Users.Where(u => u.Username == input).FirstOrDefault();
            if (user != null)
            {
                var hash = user.PasswordHash;
                var salt = user.Salt;
                //string finalString = password + salt;
                //var hashedPassword = hashComputer.GetPasswordHashAndSalt(finalString);
                var hashedPassword = pm.GeneratePasswordHash(password, salt);
                var result = new bool();
                if (hash == hashedPassword)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

                return result;
            }

            return false;
            //bool loginResult = pm.IsPasswordMatch(password, salt, hash);
            //return loginResult;
        }

    }
}
