using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Domain.Entities;

public partial class Usuario
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public DateOnly? Fechanacimiento { get; set; }

    public string? Contacto { get; set; }

    public string? Email { get; set; }

    public string? Nick { get; set; }

    public string? Pin { get; set; }

    public string? Idarchivoperfil { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Datosrecuperacion> Datosrecuperacions { get; set; } = new List<Datosrecuperacion>();

    public virtual ICollection<Hash> Hashes { get; set; } = new List<Hash>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
    public virtual ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();
    
    public static Usuario CreateFromRecord(RegisterUserDto registerUserDto)
    {
        return new Usuario
        {
            Nombre = registerUserDto.Nombre,
            Apellido = registerUserDto.Apellido,
            Email = registerUserDto.Email,
            Identificacion = registerUserDto.Identificacion,
            Estaactivo = true
        };
    }
}
