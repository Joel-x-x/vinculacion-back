namespace AnimalProtecction.Domain.Entities;

public class TiposCampana
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public bool EstaActivo { get; set; } = true;
}