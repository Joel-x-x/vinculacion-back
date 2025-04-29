namespace AnimalProtection.Domain.Entities;

public partial class Tiposcampana
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Campana> Campanas { get; set; } = new List<Campana>();
}
