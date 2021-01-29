using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface IUsersService
    {
        bool IsLoginValid(string username, string password);

        void Create(string username, string email, string pasword);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailalble(string email);
    }
}
