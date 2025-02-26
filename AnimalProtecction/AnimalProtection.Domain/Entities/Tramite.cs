using AnimalProtection.Domain.Dto;

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
    
    // Crear el método para generar el número de trámite
    public static Tramite CreateFromRecord(TramiteCreateRecord tramiteCreateRecord)
    {
        return new Tramite
        {
            Id = Guid.NewGuid(),
            Nombre = tramiteCreateRecord.Nombre,
            Apellido = tramiteCreateRecord.Apellido,
            Email = tramiteCreateRecord.Email,
            Contacto = tramiteCreateRecord.Contacto,
            Datos = tramiteCreateRecord.Datos,
            Fecha = DateTime.Now, // TODO: Revisar si la fecha debe ser la actual o la que se recibe
            Numerotramite = tramiteCreateRecord.Numerotramite, // Crear el método para generar el número de trámite
            Direccion = tramiteCreateRecord.Direccion,
            Idtipotramite = tramiteCreateRecord.Idtipotramite,
            Idestadotramite = tramiteCreateRecord.Idestadotramite,
            Estaactivo = true
        };
    }
    // Actualizar el método para generar el número de trámite
    public void UpdateFromRecord(TramiteUpdateRecord tramiteUpdateRecord)
    {
        if (!string.Equals(Nombre, tramiteUpdateRecord.Nombre, StringComparison.Ordinal))
            Nombre = tramiteUpdateRecord.Nombre;

        if (!string.Equals(Apellido, tramiteUpdateRecord.Apellido, StringComparison.Ordinal))
            Apellido = tramiteUpdateRecord.Apellido;

        if (!string.Equals(Email, tramiteUpdateRecord.Email, StringComparison.OrdinalIgnoreCase))
            Email = tramiteUpdateRecord.Email;

        if (!string.Equals(Contacto, tramiteUpdateRecord.Contacto, StringComparison.Ordinal))
            Contacto = tramiteUpdateRecord.Contacto;

        if (!string.Equals(Datos, tramiteUpdateRecord.Datos, StringComparison.Ordinal))
            Datos = tramiteUpdateRecord.Datos;

        if (!string.Equals(Direccion, tramiteUpdateRecord.Direccion, StringComparison.Ordinal))
            Direccion = tramiteUpdateRecord.Direccion;

        if (tramiteUpdateRecord.Numerotramite.HasValue && Numerotramite != tramiteUpdateRecord.Numerotramite.Value)
            Numerotramite = tramiteUpdateRecord.Numerotramite.Value;

        if (tramiteUpdateRecord.Idtipotramite.HasValue && Idtipotramite != tramiteUpdateRecord.Idtipotramite.Value)
            Idtipotramite = tramiteUpdateRecord.Idtipotramite.Value;

        if (tramiteUpdateRecord.Idestadotramite.HasValue && Idestadotramite != tramiteUpdateRecord.Idestadotramite.Value)
            Idestadotramite = tramiteUpdateRecord.Idestadotramite.Value;
    }

    public void eliminar()
    {
        Estaactivo = false;
    }
}
