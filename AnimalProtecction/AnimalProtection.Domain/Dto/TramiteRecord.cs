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
);

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
    Guid Idestadotramite,
    bool? Estaactivo
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


