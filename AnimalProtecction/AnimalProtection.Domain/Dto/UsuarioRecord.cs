namespace AnimalProtection.Domain.Dto;

public record UsuarioDto(
     Guid Id,
     string Nombre,
     string Apellido,
     DateOnly? Fechanacimiento,
     string Identificacion,
     string Contacto ,
     string Email,
     bool? Estado);
     
public record UsuarioEmailDto(
    Guid Id,
    string Nombre,
    string Apellido);