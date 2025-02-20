namespace AnimalProtection.Domain.Entities;

public partial class Adopcione
{
    public Guid Id { get; set; }

    public Guid Idmascota { get; set; }

    public Guid Idtramite { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Mascota IdmascotaNavigation { get; set; } = null!;

    public virtual Tramite IdtramiteNavigation { get; set; } = null!;
}
