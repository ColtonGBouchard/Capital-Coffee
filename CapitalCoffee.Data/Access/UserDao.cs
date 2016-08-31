using CapitalCoffee.Data.Models;
using System.Linq;
using System.Data.Entity;


namespace CapitalCoffee.Data.Access
{
    public class UserDao
    {
        private readonly CapitalCoffeeContext context;

        public UserDao(CapitalCoffeeContext context)
        {
            this.context = context;
        }

        public User GetByEmailOrUser(string input)
        {
            User user = context.Users.Where(u => u.Username == input || u.EmailAddress == input).FirstOrDefault();
            return user;
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
            User user = context.Users.Where(u => u.Username == input || u.EmailAddress == input).FirstOrDefault();
            if (user != null)
            {
                var hash = user.PasswordHash;
                var salt = user.Salt;
                var hashedPassword = pm.GeneratePasswordHash(password, salt);               
                return hash==hashedPassword;
            }

            return false;
        }

        public bool CheckPassword(int userId, string password)
        {
            HashComputer hashComputer = new HashComputer();
            PasswordManager pm = new PasswordManager();
            User user = context.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                var hash = user.PasswordHash;
                var salt = user.Salt;
                var hashedPassword = pm.GeneratePasswordHash(password, salt);
                return hash == hashedPassword;
            }

            return false;
        }

        public User GetById(int id)
        {
            return context.Users.Where(u => u.UserId == id).FirstOrDefault();
        }

        public void Edit(User user)
        {
            if (context.Entry(user).State == EntityState.Modified)
            {
                context.SaveChanges();
            }
        }

        public bool IsAdmin(User user)
        {
            if (user.RoleId == 1)
                return true;
            return false;
        }

        public void ChangePassword(int userId, string password)
        {
            var user = context.Users.Find(userId);

            HashComputer hashComputer = new HashComputer();
            PasswordManager passwordManager = new PasswordManager();
            string salt = SaltGenerator.GetSaltString();
            var hash = passwordManager.GeneratePasswordHash(password, salt);

            user.PasswordHash = hash;
            user.Salt = salt;

            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }

        public User GetEmail(string emailAddress)
        {
            var user = context.Users.Where(u => u.EmailAddress == emailAddress).FirstOrDefault();
            return user;
        }
      
    }
}
