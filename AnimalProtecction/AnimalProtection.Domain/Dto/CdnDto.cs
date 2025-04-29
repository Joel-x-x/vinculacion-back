using Microsoft.AspNetCore.Http;

namespace AnimalProtection.Domain.Dto;

public class UploadRequestDto
{
    public IFormFile File { get; set; }
    public string Directory { get; set; }
}

public class UploadsRequestDto
{
    public IEnumerable<UploadFilesRequestDto> Files { get; set; }
}

public class UploadFilesRequestDto
{
    public string DirectoryName { get; set; }
    public string FileName { get; set; }
    public string Base64Content { get; set; }
}