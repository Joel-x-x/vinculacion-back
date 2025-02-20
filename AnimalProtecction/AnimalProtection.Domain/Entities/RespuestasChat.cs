namespace AnimalProtection.Domain.Entities;

public partial class Respuestaschat
{
    public Guid Id { get; set; }

    public string Respuesta { get; set; } = null!;

    public Guid Idpregunta { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Preguntaschat IdpreguntaNavigation { get; set; } = null!;
}
