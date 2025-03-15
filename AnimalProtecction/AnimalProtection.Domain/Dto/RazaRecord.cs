using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record RazaRecord
(
    Guid Id,
    string Nombre,
    bool? Estaactivo
    )
{
    public RazaRecord(Raza raza) : this(
        raza.Id,
        raza.Nombre,
        raza.Estaactivo
    )
    { }
}

public record RazaCreateRecord(
    string Nombre,
    bool? Estaactivo
);

public record RazaUpdateRecord(
    Guid Id,
    string Nombre,
    bool? Estaactivo
);