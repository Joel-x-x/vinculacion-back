using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record CooperantesRecord(
    Guid Id,
    string Nombre,
    string Descripcion,
    string ColorSecundario,
    Guid? IdArchivoLogo,
    bool EstaActivo
)
{
    public CooperantesRecord(Cooperantes cooperante) : this (
        cooperante.Id,
        cooperante.Nombre,
        cooperante.Descripcion,
        cooperante.ColorSecundario,
        cooperante.IdArchivoLogo,
        cooperante.EstaActivo
    )
    {}
}

public record CooperantesCreateRecord(
    Guid Id,
    string Nombre,
    string Descripcion,
    string ColorSecundario,
    Guid? IdArchivoLogo,
    bool EstaActivo
);

public record CooperantesUpdateRecord(
    Guid Id,
    string Nombre,
    string Descripcion,
    string ColorSecundario,
    Guid? IdArchivoLogo,
    bool EstaActivo
);