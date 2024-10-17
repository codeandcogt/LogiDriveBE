namespace LogiDriveBE.AUTH.Aao
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string email, string password);
    }
}
