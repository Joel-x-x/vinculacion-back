using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Domain.Entities;

public partial class Tipostramite
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Tramite> Tramites { get; set; } = new List<Tramite>();

    public void eliminar()
    {
        this.Estaactivo = false;
    }

    public void UpdateFromRecord(TiposTramiteUpdateRecord tipostramiteUpdateRecord)
    {
        if (tipostramiteUpdateRecord.Nombre != null)
        {
            this.Nombre = tipostramiteUpdateRecord.Nombre;
        }
    }

    public static Tipostramite CreateFromRecord(TiposTramiteCreateRecord tipostramiteCreateRecord)
    {
        return new Tipostramite
        {
            Nombre = tipostramiteCreateRecord.Nombre,
            Estaactivo = true
        };
    }
}
