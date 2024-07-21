using G_APIs.Models;

namespace G_APIs.BussinesLogic.Interface;

public interface IAccount
{
        Task<ApiResult> Login(User model);
        Task<ApiResult> SetPassword(User model);
        Task<ApiResult> CompleteProfile(User model);
        Task<ApiResult> SignUp(User model);
}
