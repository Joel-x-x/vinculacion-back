namespace AnimalProtection.Domain.Entities;

public partial class Tiposarchivo
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Archivo> Archivos { get; set; } = new List<Archivo>();

   
    public void Delete()
    {
        Estaactivo = false;
    }
}
