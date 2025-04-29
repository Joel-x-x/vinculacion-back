using AnimalProtection.Application.Commands.Cdn;
using AnimalProtection.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalProtection.Api.Controller;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly IOrquestadorService _orchesterService;

    public FileController(IOrquestadorService orchesterService)
    {
        _orchesterService = orchesterService;
    }

    /// <summary>
    /// UploadFile
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] UploadRequestDto request)
    {
        // Subimos el archivo a GitHub y obtenemos la URL
        var response = await _orchesterService.UploadFileToGitHub(request);

        return string.IsNullOrWhiteSpace(response.path)
            ? BadRequest(new
            {
                message = response.mensaje
            })
            : Ok(new
            {
                message = response.mensaje,
                url = response.path
            });
    }

    /// <summary>
    /// UploadFiles
    /// </summary>
    /// <param name="files"></param>
    /// {
    //   "files": [
    //     {
    //       "directoryName": "landing",
    //       "fileName": "1.png",
    //       "base64Content": "base64Content"
    //     },
    //     {
    //       "directoryName": "landing",
    //       "fileName": "3.png",
    //       "base64Content": "base64Content"
    //     },
    //    {
    //       "directoryName": "landing",
    //       "fileName": "2.png",
    //       "base64Content": "base64Content"
    //     }
    //   ]
    // }
    /// <returns>string path</returns>
    [HttpPost("uploads")]
    public async Task<IActionResult> UploadFile([FromBody] UploadsRequestDto files)
    {
        var response = await _orchesterService.UploadFilesToGitHub(files);

        return response.paths == null
            ? BadRequest(new
            {
                message = response.mensaje
            })
            : Ok(new
            {
                message = response.mensaje,
                url = response.paths
            });
    }

    /// <summary>
    /// UploadFiles
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("upload-base")]
    public async Task<IActionResult> UploadFileBase64([FromBody] UploadFilesRequestDto file)
    {
        var response = await _orchesterService.UploadFilesToGitHub(file);

        return response.path == null
            ? BadRequest(new
            {
                message = response.mensaje
            })
            : Ok(new
            {
                message = response.mensaje,
                url = response.path
            });
    }
}