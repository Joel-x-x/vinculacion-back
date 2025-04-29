using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Application.Commands.Cdn;

public interface IOrquestadorService
{
    Task<(string mensaje, string path)> UploadFileToGitHub(UploadRequestDto request);
    Task<(string mensaje, List<string> paths)> UploadFilesToGitHub(UploadsRequestDto files);
    Task<(string mensaje, string path)> UploadFilesToGitHub(UploadFilesRequestDto file);
}