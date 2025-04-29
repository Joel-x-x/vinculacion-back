namespace AnimalProtection.Application.Commands.Cdn;

public interface IGitHubService
{
    Task<string> UploadFileToGitHub(string filePath, string fileName, string directory);
}