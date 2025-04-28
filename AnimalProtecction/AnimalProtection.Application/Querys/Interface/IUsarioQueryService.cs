using AnimalProtection.Domain.Dto;
using AnimalProtection.Domain.Result;

namespace AnimalProtection.Application.Querys.Interface;

public interface IUsuarioQueryService
{

    Task<ResultResponse<List<UsuarioDto>>> GetAllUser();
    Task<UsuarioDto>GetUserById();

    Task<ResultResponse<Boolean>> RegisterUser(
        RegisterUserDto registerUserDto);
    Task<ResultResponse<UsuarioDto>> Login(
        LoginUserDto loginUserDto);
    Task<UsuarioDto>GetUserByIdentification();
    
    Task<ResultResponse<UsuarioEmailDto>>GetUserByEmail(string email);
}