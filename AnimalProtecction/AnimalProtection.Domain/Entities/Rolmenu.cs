namespace AnimalProtection.Domain.Entities;

public partial class Rolmenu
{
    public Guid Id { get; set; }

    public Guid RolId { get; set; }

    public Guid MenuId { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual Rol Rol { get; set; } = null!;
    

}
