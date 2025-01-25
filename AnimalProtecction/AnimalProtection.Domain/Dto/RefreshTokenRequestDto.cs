namespace AnimalProtection.Domain.Dto;

public record RefreshTokenRequestDto(
    string ExpiredToken,
    string RefreshToken);