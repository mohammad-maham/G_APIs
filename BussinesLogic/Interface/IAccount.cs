using G_APIs.Models;

namespace G_APIs.BussinesLogic.Interface;

public interface IAccount
{
        Task<User?> Login(string username, string password);
        Task<User?> Register(User model);
}
