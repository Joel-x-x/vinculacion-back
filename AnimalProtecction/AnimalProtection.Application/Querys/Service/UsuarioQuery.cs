using AnimalProtecction.Generated.Repositories.Interface;
using AnimalProtection.Application.Querys.Interface;
using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Entities;
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
                u.Estaactivo
            )).ToList();
            return ResultResponse<List<UsuarioDto>>.Success(usuariosDto);
        }
        return ResultResponse<List<UsuarioDto>>.Failure("No se encontraron usuarios", 404);
    }

    public Task<UsuarioDto> GetUserById()
    {
        throw new NotImplementedException();
    }

    public async Task<ResultResponse<Boolean>> RegisterUser(RegisterUserDto 
        registerUserDto)
    {
        // TODO: Validar los datos para el registro
        
        // Hashea la contraseña directamente usando BCrypt
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto
            .Clave);
        
        // Registrar un nuevo usuario
        var user = Usuario.CreateFromRecord(registerUserDto,
            passwordHash);
        
        // Guardar el usuario en la base de datos
        await _usuarioRepository.AddAsync(user);
        await _usuarioRepository.SaveAsync();
        
        // Devolver el usuario registrado
        return ResultResponse<Boolean>.Success(true, 201);
    }

    public async Task<ResultResponse<UsuarioDto>> Login(LoginUserDto loginUserDto)
    {   
        // TODO: Validar los datos para el login
        // Buscar el usuario en la base de datos
        var userResult = await _usuarioRepository.GetByEmail(loginUserDto.Email);
        // Verificar si la búsqueda fue exitosa
        if (!userResult.IsSuccess)
        {
            return ResultResponse<UsuarioDto>.Failure("Usuario no encontrado", 
                404);
        }
        
        // Obtener el usuario del ResultResponse
        var user = userResult.Value;

        // Validar la contraseña
        bool passwordIsValid = BCrypt.Net.BCrypt.Verify(loginUserDto
            .Clave, user.Clave);

        if (!passwordIsValid)
        {
            return ResultResponse<UsuarioDto>.Failure("Contraseña incorrecta", 401);
        }

        // 4. Retornar éxito
        return ResultResponse<UsuarioDto>.Success(new UsuarioDto(user), 200);
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