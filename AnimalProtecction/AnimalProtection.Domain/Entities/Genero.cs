using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Domain.Entities;

public partial class Genero
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();

    public static Genero CreateFromRecord(GeneroCreateRecord generoCreateRecord)
    {
        return new Genero
        {
            Nombre = generoCreateRecord.Nombre,
            Estaactivo = true
        };
    }

    public void UpdateFromRecord(GeneroUpdateRecord generoUpdateRecord)
    {
        if (generoUpdateRecord.Nombre != null)
        {
            Nombre = generoUpdateRecord.Nombre;
        }
        if (generoUpdateRecord.Estaactivo != null)
        {
            Estaactivo = generoUpdateRecord.Estaactivo;
        }
    }

    public void Delete()
    {
        Estaactivo = false;
    }
}