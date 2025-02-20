
namespace AnimalProtection.Domain.Entities;

public partial class Archivo
{
    public Guid Id { get; set; }

    public string Url { get; set; } = null!;

    public string Formato { get; set; } = null!;

    public Guid Idtipoarchivo { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Tiposarchivo IdtipoarchivoNavigation { get; set; } = null!;
}
