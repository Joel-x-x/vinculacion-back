namespace AnimalProtection.Domain.Entities;

public partial class UsuarioRol
{
    public Guid Id { get; set; }

    public Guid Idusuario { get; set; }

    public Guid Idrol { get; set; }

    public DateTime Fechaasignacion { get; set; } = DateTime.UtcNow;

    public bool Estaactivo { get; set; } = true;

    public virtual Usuario Usuario { get; set; } = null!;

    public virtual Rol Rol { get; set; } = null!;
}