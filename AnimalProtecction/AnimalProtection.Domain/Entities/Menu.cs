namespace AnimalProtection.Domain.Entities;

public partial class Menu
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Link { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Menusrol> Menusrols { get; set; } = new List<Menusrol>();
}
