namespace AnimalProtection.Domain.Entities;

public class Campanas
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Cuerpo { get; set; }
    public DateTime? FechaEvento { get; set; }
    public DateTime? FechaCaducidad { get; set; }
    public Guid IdTipoCampana { get; set; }
    public bool EstaActivo { get; set; } = true;

    // Relación con TiposCampana
    public Tiposcampana TipoCampana { get; set; }
}