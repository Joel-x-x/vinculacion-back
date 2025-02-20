namespace AnimalProtection.Domain.Entities;

public partial class Datosrecuperacion
{
    public Guid Id { get; set; }

    public string Pregunta { get; set; } = null!;

    public string? Respuesta { get; set; }

    public Guid Idusuario { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
