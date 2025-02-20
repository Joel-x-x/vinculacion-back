namespace AnimalProtection.Domain.Entities;

public partial class Preguntasfrecuente
{
    public Guid Id { get; set; }

    public string Pregunta { get; set; } = null!;

    public string Respuesta { get; set; } = null!;

    public int? Prioridad { get; set; }

    public bool? Estaactivo { get; set; }
}
