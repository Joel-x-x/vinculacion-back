namespace AnimalProtection.Domain.Entities;

public partial class Tiposlog
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
