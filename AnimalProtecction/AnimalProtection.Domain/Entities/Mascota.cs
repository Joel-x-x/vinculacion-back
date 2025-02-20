namespace AnimalProtection.Domain.Entities;

public partial class Mascota
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public string? Caracter { get; set; }

    public string? Detalles { get; set; }

    public Guid Idgenero { get; set; }

    public Guid Idespecie { get; set; }

    public Guid Idraza { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Adopcione> Adopciones { get; set; } = new List<Adopcione>();

    public virtual ICollection<Archivosmascotum> Archivosmascota { get; set; } = new List<Archivosmascotum>();

    public virtual Especy IdespecieNavigation { get; set; } = null!;

    public virtual Genero IdgeneroNavigation { get; set; } = null!;

    public virtual Raza IdrazaNavigation { get; set; } = null!;
}
