using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Application.Commands.Cdn;

public class OrquestadorService: IOrquestadorService
{
    private readonly IGitHubService _gitHubService;

    public OrquestadorService(IGitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }
    
    public async Task<(string mensaje,string path)> UploadFileToGitHub(UploadRequestDto request)
    {
        var file = request.File;
        if (file == null || file.Length == 0)
            return (mensaje:"Archivo no proporcionado.",path:string.Empty);

        var filePath = Path.Combine(Path.GetTempPath(), file.FileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Subimos el archivo a GitHub y obtenemos la URL
        var fileUrl = await _gitHubService.UploadFileToGitHub(filePath, file.FileName, request.Directory);

        // Eliminamos el archivo temporal
        File.Delete(filePath);
        
        return (mensaje:"Archivo subido a GitHub exitosamente.",path:fileUrl);
    }
    
    public async Task<(string mensaje, List<string> paths)> UploadFilesToGitHub(UploadsRequestDto files)
    {
        var pathList = new List<string>();
        var filesToProcess = files.Files;
    
        if (filesToProcess == null || !filesToProcess.Any())
            return (mensaje: "Archivo no proporcionado.", path: null);

        foreach (var file in filesToProcess)
        {
            if (string.IsNullOrWhiteSpace(file.Base64Content))
                continue;

            // 1. Decodificar el contenido base64
            var fileBytes = Convert.FromBase64String(file.Base64Content);

            // 2. Crear un archivo temporal en el servidor
            var tempFilePath = Path.Combine(Path.GetTempPath(), file.FileName);
            await File.WriteAllBytesAsync(tempFilePath, fileBytes);

            try
            {
                // 3. Subir el archivo a GitHub
                var fileUrl = await _gitHubService.UploadFileToGitHub(tempFilePath, file.FileName, file.DirectoryName);
                pathList.Add(fileUrl);
            }
            finally
            {
                // 4. Borrar el archivo temporal
                if (File.Exists(tempFilePath))
                    File.Delete(tempFilePath);
            }
        }

        return (mensaje: "Archivos subidos a GitHub exitosamente.", paths: pathList);
    }
    
    public async Task<(string mensaje, string path)> UploadFilesToGitHub(UploadFilesRequestDto file)
    {
        var fileUrl = string.Empty;
        if (string.IsNullOrWhiteSpace(file.Base64Content))
            return (mensaje: "Archivo no proporcionado.", path: null);

            // 1. Decodificar el contenido base64
            var fileBytes = Convert.FromBase64String(file.Base64Content);

            // 2. Crear un archivo temporal en el servidor
            var tempFilePath = Path.Combine(Path.GetTempPath(), file.FileName);
            await File.WriteAllBytesAsync(tempFilePath, fileBytes);

            try
            {
                // 3. Subir el archivo a GitHub
                fileUrl = await _gitHubService.UploadFileToGitHub(tempFilePath, file.FileName, file.DirectoryName);
            }
            finally
            {
                // 4. Borrar el archivo temporal
                if (File.Exists(tempFilePath))
                    File.Delete(tempFilePath);
            }

        return (mensaje: "Archivos subidos a GitHub exitosamente.", paths: fileUrl);
    }
}