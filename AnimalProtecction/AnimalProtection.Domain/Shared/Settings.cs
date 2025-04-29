namespace AnimalProtection.Domain.Shared;

public class Settings
{
    public string ApiKey { get; set; } = string.Empty;
    public string UrlApi { get; set; } = string.Empty;
    public string RecaptchaToken { get; set; } = string.Empty;
}