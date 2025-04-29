using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Domain.Entities;

public partial class Estadostramite
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Orden { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Tramite> Tramites { get; set; } = new List<Tramite>();

    public static Estadostramite CreateFromRecord(EstadostramiteCreateRecord estadostramiteCreateRecord)
    {
        return new Estadostramite
        {
            Nombre = estadostramiteCreateRecord.Nombre,
            Orden = estadostramiteCreateRecord.Orden,
            Estaactivo = true
        };
    }

    public void UpdateFromRecord(EstadostramiteUpdateRecord estadostramiteUpdateRecord)
    {
        if (estadostramiteUpdateRecord.Nombre != null)
        {
            this.Nombre = estadostramiteUpdateRecord.Nombre;
        }
        if (estadostramiteUpdateRecord.Orden != null)
        {
            this.Orden = estadostramiteUpdateRecord.Orden.Value;
        }
    }

    public void eliminar()
    {
        this.Estaactivo = false;
    }
}
