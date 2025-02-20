namespace AnimalProtection.Domain.Entities;

public partial class RoleUsuario
{
    public Guid Id { get; set; }

    public Guid UsuarioId { get; set; }

    public Guid RolId { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public virtual Rol Rol { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
