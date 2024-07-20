using G_APIs.Models;

namespace G_APIs.BussinesLogic.Interface;

public interface IAccount
{
        Task<User?> Login(string username, string password);
        Task<User?> Register(User model);
        Task<User?> GetConfirmCode(User mobile);
        Task<User?> SetPassword(User mobile);
        Task<User?> CompleteProfile(User model);
}
