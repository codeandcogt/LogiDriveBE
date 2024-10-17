using LogiDriveBE.AUTH.Aao;

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

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _authAao.GetUserByEmailAsync(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            var roles = await _authAao.GetUserRolesAsync(user.IdAppUser);
            var permissions = await _authAao.GetUserPermissionsAsync(user.IdAppUser);

            return _jwtService.GenerateToken(user, roles, permissions);
        }
    }
}
