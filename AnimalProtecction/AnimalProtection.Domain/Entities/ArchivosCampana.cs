namespace AnimalProtection.Domain.Entities;

public partial class Archivoscampana
{
    public Guid Id { get; set; }

    public Guid Idarchivo { get; set; }

    public string Descripcion { get; set; } = null!;

    public Guid Idcampana { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Campana IdcampanaNavigation { get; set; } = null!;

    public virtual Archivo IdarchivoNavigation { get; set; } = null!;
}
