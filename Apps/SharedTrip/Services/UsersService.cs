using SharedTrip.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SharedTrip.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public void Create(string username, string email, string pasword)
        {
            var user = new User
            {
                Email = email,
                Username = username,
                Password = ComputeHash(pasword),
            };
            this.db.Users.Add(user);
            this.db.SaveChanges();
        }

        public bool IsEmailAvailalble(string email)
        {
            return !this.db.Users.Any(x => x.Email == email);
        }

        public bool IsLoginValid(string username, string password)
        {
            var hashPassword = ComputeHash(password);
            return this.db.Users.Any(x => x.Username == username && x.Password == password);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }

        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    
}

