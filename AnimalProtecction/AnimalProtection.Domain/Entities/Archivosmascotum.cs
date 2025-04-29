namespace AnimalProtection.Domain.Entities;

public partial class Archivosmascotum
{
    public Guid Id { get; set; }

    public string? Descripcion { get; set; }

    public Guid Idarchivo { get; set; }

    public int? Orden { get; set; }

    public bool? Esprincipal { get; set; }

    public Guid Idmascota { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Mascota IdmascotaNavigation { get; set; } = null!;
}
