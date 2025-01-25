using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Service;

public class UsuarioQueryService: IUsuarioQueryService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioQueryService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ResultResponse<List<UsuarioDto>>> GetAllUser()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        if (usuarios.Any())
        {
            var usuariosDto = usuarios.Select(u => new UsuarioDto
            (
                u.Id,
                u.Nombre,
                u.Apellido,
                u.Fechanacimiento,
                u.Identificacion,
                u.Contacto,
                u.Email,
                u.Estado
            )).ToList();
            return ResultResponse<List<UsuarioDto>>.Success(usuariosDto);
        }
        return ResultResponse<List<UsuarioDto>>.Failure("No se encontraron usuarios", 404);
    }

    public Task<UsuarioDto> GetUserById()
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioDto> GetUserByIdentification()
    {
        throw new NotImplementedException();
    }

    public async Task<ResultResponse<UsuarioEmailDto>> GetUserByEmail(string email)
    {
       var usuarioEncontrado = await _usuarioRepository.GetByEmail(email);
       if (usuarioEncontrado.IsSuccess)
       {
            var usuarioDto = new UsuarioEmailDto
            (
                usuarioEncontrado.Value.Id,
                usuarioEncontrado.Value.Nombre,
                usuarioEncontrado.Value.Apellido
            );
            return ResultResponse<UsuarioEmailDto>.Success(usuarioDto);
       }

       return ResultResponse<UsuarioEmailDto>.Failure(usuarioEncontrado.Error,404);
    }
}