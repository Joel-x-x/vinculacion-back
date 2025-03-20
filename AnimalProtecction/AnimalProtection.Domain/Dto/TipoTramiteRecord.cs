using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record TiposTramiteRecord(
    Guid Id,
    string Nombre,
    bool Estaactivo
)
{
    public TiposTramiteRecord(Tipostramite tipostramite) : this(tipostramite.Id, tipostramite.Nombre, tipostramite.Estaactivo??false)
    {
    }
};

public record TiposTramiteCreateRecord(
    string Nombre
);

public record TiposTramiteUpdateRecord(
    Guid Id,
    string? Nombre
);