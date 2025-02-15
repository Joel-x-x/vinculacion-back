namespace AnimalProtecction.Domain.Entities;

public class ArchivosCampana
{
    public Guid Id { get; set; }
    public Guid IdArchivo { get; set; }
    public string Descripcion { get; set; }
    public Guid IdCampana { get; set; }
    public bool EstaActivo { get; set; } = true;

    // Relación con Campanas
    public Campanas Campana { get; set; }
}