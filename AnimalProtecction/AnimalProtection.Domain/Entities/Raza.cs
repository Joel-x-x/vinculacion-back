using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Domain.Entities;

public partial class Raza
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();

    public static Raza CreateFromRecord(RazaCreateRecord razaCreateRecord)
    {
        return new Raza
        {
            Nombre = razaCreateRecord.Nombre,
            Estaactivo = true
        };
    }

    public void UpdateFromRecord(RazaUpdateRecord razaUpdateRecord)
    {
        if (razaUpdateRecord.Nombre != null)
        {
            Nombre = razaUpdateRecord.Nombre;
        }
        if (razaUpdateRecord.Estaactivo != null)
        {
            Estaactivo = razaUpdateRecord.Estaactivo;
        }
    }

    public void Delete()
    {
        Estaactivo = false;
    }
}