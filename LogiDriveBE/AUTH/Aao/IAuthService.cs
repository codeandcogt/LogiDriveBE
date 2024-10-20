using LogiDriveBE.UTILS;

namespace LogiDriveBE.AUTH.Aao
{
    public interface IAuthService
    {
        Task<OperationResponse<string>> AuthenticateAsync(string email, string password);
    }
}
