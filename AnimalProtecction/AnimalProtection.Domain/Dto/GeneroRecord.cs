using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record GeneroRecord(
    Guid Id,
    string Nombre,
    bool? Estaactivo
    )
{
    public GeneroRecord(Genero genero) : this(
        genero.Id,
        genero.Nombre,
        genero.Estaactivo
    )
    { }
}

public record GeneroCreateRecord(
    Guid? Id,
    string Nombre,
    bool? Estaactivo
);

public record GeneroUpdateRecord(
    Guid Id,
    string Nombre,
    bool? Estaactivo
);