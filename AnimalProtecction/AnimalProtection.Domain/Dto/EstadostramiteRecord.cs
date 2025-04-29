using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record EstadostramiteRecord(
    Guid Id,
    string Nombre,
    int Orden,
    bool? Estaactivo
)
{
    public EstadostramiteRecord(Estadostramite estadostramite) : this(estadostramite.Id, estadostramite.Nombre, estadostramite.Orden, estadostramite.Estaactivo)
    {
    }
};

public record EstadostramiteCreateRecord(
    string Nombre,
    int Orden
);

public record EstadostramiteUpdateRecord(
    Guid Id,
    string? Nombre,
    int? Orden
);