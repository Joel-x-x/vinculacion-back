namespace AnimalProtection.Domain.Entities;

public partial class Preguntaschat
{
    public Guid Id { get; set; }

    public string Pregunta { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Respuestaschat> Respuestaschats { get; set; } = new List<Respuestaschat>();
}
