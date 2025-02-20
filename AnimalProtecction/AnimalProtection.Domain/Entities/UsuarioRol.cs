namespace AnimalProtection.Domain.Entities;

public partial class UsuarioRol
{
    public Guid Id { get; set; }

    public Guid UsuarioId { get; set; }

    public Guid RolId { get; set; }

    public DateTime? FechaAsignacion { get; set; }
    
    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Role Rol { get; set; } = null!;

}
