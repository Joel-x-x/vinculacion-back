namespace AnimalProtection.Domain.Entities;

public partial class Tramite
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Contacto { get; set; }

    public string? Datos { get; set; }

    public DateTime Fecha { get; set; }

    public int Numerotramite { get; set; }

    public string? Direccion { get; set; }

    public Guid Idtipotramite { get; set; }

    public Guid Idestadotramite { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Adopcione> Adopciones { get; set; } = new List<Adopcione>();

    public virtual ICollection<Archivostramite> Archivostramites { get; set; } = new List<Archivostramite>();

    public virtual Estadostramite IdestadotramiteNavigation { get; set; } = null!;

    public virtual Tipostramite IdtipotramiteNavigation { get; set; } = null!;
}
