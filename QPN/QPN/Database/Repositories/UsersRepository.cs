using QPN.Database.Models;
using System.Linq;

namespace QPN.Database.Repositories
{
    public class UsersRepository
    {
        public User GetUser(string login, string password)
        {
            return DatabaseService.Query<User>($"SELECT * from users WHERE login=@login AND password=@password",
                new
                {
                    login,
                    password
                }).FirstOrDefault();
        }
    }
}