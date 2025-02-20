namespace AnimalProtection.Domain.Entities;

public partial class Cooperante
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Colorsecundario { get; set; } = null!;

    public Guid? Idarchivologo { get; set; }

    public bool? Estaactivo { get; set; }
}
