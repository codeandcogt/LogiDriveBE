using LogiDriveBE.AUTH.Aao;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.AUTH.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthAao _authAao;
        private readonly IJwtService _jwtService;

        public AuthService(IAuthAao authAao, IJwtService jwtService)
        {
            _authAao = authAao;
            _jwtService = jwtService;
        }

        public async Task<OperationResponse<string>> AuthenticateAsync(string email, string password)
        {
            var user = await _authAao.GetUserByEmailAsync(email);

            if (user == null)
            {
                return new OperationResponse<string>(401, "User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return new OperationResponse<string>(401, "Invalid password");
            }

            var roles = await _authAao.GetUserRolesAsync(user.IdAppUser);
            var permissions = await _authAao.GetUserPermissionsAsync(user.IdAppUser);

            var token = _jwtService.GenerateToken(user, roles, permissions);

            return new OperationResponse<string>(200, "Login successful", token);
        }
    }
}
