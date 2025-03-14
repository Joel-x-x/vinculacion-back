using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record TramiteRecord(
    Guid Id,
    string Nombre,
    string Apellido,
    string email,
    string Contacto,
    string Datos,
    DateTime Fecha,
    int Numerotramite,
    string Direccion,
    Guid Idtipotramite,
    Guid Idestadotramite,
    bool Estaactivo,
    TipoTramiteRecord Tipotramite,
    EstadostramiteRecord Estadostramite,
    List<ArchivostramiteRecord> Archivostramites
)
{
    public TramiteRecord(Tramite tramite) : this(
        tramite.Id,
        tramite.Nombre,
        tramite.Apellido,
        tramite.Email,
        tramite.Contacto ?? "",
        tramite.Datos ?? "",
        tramite.Fecha,
        tramite.Numerotramite,
        tramite.Direccion ?? "",
        tramite.Idtipotramite,
        tramite.Idestadotramite,
        tramite.Estaactivo ?? false,
        new TipoTramiteRecord(
            tramite.Idtipotramite,
            tramite.IdtipotramiteNavigation?.Nombre ?? "",
            tramite.IdtipotramiteNavigation?.Estaactivo ?? false
        ),
        new EstadostramiteRecord(
            tramite.Idestadotramite,
            tramite.IdestadotramiteNavigation?.Nombre ?? "",
            tramite.IdestadotramiteNavigation?.Orden ?? 0,
            tramite.IdestadotramiteNavigation?.Estaactivo ?? false
        ),
        tramite.Archivostramites?
            .Select(a => new ArchivostramiteRecord(a.Id,
                a.Idarchivo,
                a.Descripcion,
                a.Idtramite,
                a.Estaactivo))
            .ToList() ?? new List<ArchivostramiteRecord>()
    )
    { }
}

public record TramiteCreateRecord(
    string Nombre,
    string Apellido,
    string Email,
    string? Contacto,
    string? Datos,
    DateTime Fecha,
    int Numerotramite,
    string? Direccion,
    Guid Idtipotramite,
    Guid Idestadotramite
);

public record TramiteUpdateRecord(
    Guid Id,
    string? Nombre,
    string? Apellido,
    string? Email,
    string? Contacto,
    string? Datos,
    DateTime? Fecha,
    int? Numerotramite,
    string? Direccion,
    Guid? Idtipotramite,
    Guid? Idestadotramite
);


