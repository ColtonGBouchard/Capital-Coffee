﻿using CapitalCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


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


       
    }
}
