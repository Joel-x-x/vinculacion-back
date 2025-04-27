using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record UsuarioDto(
    Guid Id,
    string Nombre,
    string Apellido,
    DateOnly? Fechanacimiento,
    string Identificacion,
    string Contacto,
    string Email,
    bool? Estaactivo
)
{
    public UsuarioDto(Usuario usuario) : this(
        usuario.Id,
        usuario.Nombre,
        usuario.Apellido,
        usuario.Fechanacimiento,
        usuario.Identificacion,
        usuario.Contacto ?? "",
        usuario.Email,
        usuario.Estaactivo
    )
    { }
}

public record RegisterUserDto(
    string Nombre,
    string Apellido,
    string Email,
    string Identificacion
); 

public record LoginUserDto(
    string Email,
    string Password
); 
     
public record UsuarioEmailDto(
    Guid Id,
    string Nombre,
    string Apellido
);