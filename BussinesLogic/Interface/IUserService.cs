using G_APIs.Models;

namespace G_APIs.BussinesLogic.Interface;

public interface IUserService
{
        Task<User?> Authenticate(string username, string password);
}
