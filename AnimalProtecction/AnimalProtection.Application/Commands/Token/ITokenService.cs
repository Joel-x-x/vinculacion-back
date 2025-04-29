namespace AnimalProtection.Application.Commands.Token;

public interface ITokenService
{
    string GenerateJwtToken(String userId);
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string refreshToken, string userId);
    string RenewToken(string expiredToken, string refreshToken);
}