namespace AnimalProtection.Domain.Entities;

public partial class Campana
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Cuerpo { get; set; } = null!;

    public DateTime? Fechaevento { get; set; }

    public DateTime? Fechacaducidad { get; set; }

    public Guid Idtipocampana { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Archivoscampana> Archivoscampanas { get; set; } = new List<Archivoscampana>();

    public virtual Tiposcampana IdtipocampanaNavigation { get; set; } = null!;
}
