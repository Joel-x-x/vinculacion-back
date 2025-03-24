using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record AdopcionesRecord(
    Guid Id,
    Guid Idmascota,
    Guid Idtramite,
    bool? Estaactivo
    )
{
    public AdopcionesRecord(Adopcione adopcion) : this(
        adopcion.Id,
        adopcion.Idmascota,
        adopcion.Idtramite,
        adopcion.Estaactivo
    )
    { }
}

public record AdopcionesCreateRecord(
    Guid Idmascota,
    Guid Idtramite
);

public record AdopcionesUpdateRecord(
    Guid Id,
    Guid? Idmascota,
    Guid? Idtramite,
    bool Estaactivo
);