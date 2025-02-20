namespace AnimalProtection.Domain.Entities;

public partial class Genero
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();
}
