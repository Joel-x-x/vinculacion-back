using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Domain.Entities;

public partial class Mascota
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public string? Caracter { get; set; }

    public string? Detalles { get; set; }

    public Guid Idgenero { get; set; }

    public Guid Idespecie { get; set; }

    public Guid Idraza { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Adopcione> Adopciones { get; set; } = new List<Adopcione>();

    public virtual ICollection<Archivosmascotum> Archivosmascota { get; set; } = new List<Archivosmascotum>();

    public virtual Especy IdespecieNavigation { get; set; } = null!;

    public virtual Genero IdgeneroNavigation { get; set; } = null!;

    public virtual Raza IdrazaNavigation { get; set; } = null!;

    public static Mascota CreateFromRecord(MascotaCreateRecord mascotaCreateRecord)
    {
        return new Mascota
        {
            Id = mascotaCreateRecord.Id ?? Guid.NewGuid(),
            Nombre = mascotaCreateRecord.Nombre,
            Edad = mascotaCreateRecord.Edad,
            Caracter = mascotaCreateRecord.Caracter,
            Detalles = mascotaCreateRecord.Detalles,
            Idgenero = mascotaCreateRecord.Idgenero,
            Idespecie = mascotaCreateRecord.Idespecie,
            Idraza = mascotaCreateRecord.Idraza,
            Estaactivo = mascotaCreateRecord.Estaactivo
        };
    }

    public void UpdateFromRecord(MascotaUpdateRecord mascotaUpdateRecord)
    {
        if (mascotaUpdateRecord.Nombre != null)
        {
            Nombre = mascotaUpdateRecord.Nombre;
        }
        if (mascotaUpdateRecord.Edad != null)
        {
            Edad = mascotaUpdateRecord.Edad;
        }
        if (mascotaUpdateRecord.Caracter != null)
        {
            Caracter = mascotaUpdateRecord.Caracter;
        }
        if (mascotaUpdateRecord.Detalles != null)
        {
            Detalles = mascotaUpdateRecord.Detalles;
        }
        if (mascotaUpdateRecord.Idgenero != null)
        {
            Idgenero = mascotaUpdateRecord.Idgenero;
        }
        if (mascotaUpdateRecord.Idespecie != null)
        {
            Idespecie = mascotaUpdateRecord.Idespecie;
        }
        if (mascotaUpdateRecord.Idraza != null)
        {
            Idraza = mascotaUpdateRecord.Idraza;
        }
        if (mascotaUpdateRecord.Estaactivo != null)
        {
            Estaactivo = mascotaUpdateRecord.Estaactivo;
        }
    }

    public void Delete()
    {
        Estaactivo = false;
    }
}