namespace AnimalProtection.Domain.Dto;

public record EstadostramiteRecord(
    Guid Id,
    string Nombre,
    int Orden,
    bool? Estaactivo
);

public record EstadostramiteCreateRecord(
    string Nombre,
    int Orden
);

public record EstadostramiteUpdateRecord(
    Guid Id,
    string? Nombre,
    int? Orden
);