namespace AnimalProtection.Domain.Entities;

public partial class Archivostramite
{
    public Guid Id { get; set; }

    public Guid Idarchivo { get; set; }

    public string? Descripcion { get; set; }

    public Guid Idtramite { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Tramite IdtramiteNavigation { get; set; } = null!;
}
