namespace AnimalProtection.Domain.Entities;

public partial class Tipostramite
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Tramite> Tramites { get; set; } = new List<Tramite>();
}
