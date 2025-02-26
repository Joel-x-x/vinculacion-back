using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record TipoTramiteRecord(
    Guid Id,
    string Nombre,
    bool Estaactivo
);

public record TipoTramiteCreateRecord(
    string Nombre,
    bool? Estaactivo
);

public record TipoTramiteUpdateRecord(
    Guid Id,
    string? Nombre,
    bool? Estaactivo
);