using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record MascotaRecord
(
    Guid Id,
    string Nombre,
    int Edad,
    string? Caracter,
    string? Detalles,
    Guid Idgenero,
    Guid Idespecie,
    Guid Idraza,
    bool? Estaactivo
    )
{
    public MascotaRecord(Mascota mascota) : this(
       mascota.Id,
       mascota.Nombre,
       mascota.Edad,
       mascota.Caracter,
       mascota.Detalles,
       mascota.Idgenero,
       mascota.Idespecie,
       mascota.Idraza,
       mascota.Estaactivo
   )
    { }
}

public record MascotaCreateRecord(
    string Nombre,
    int Edad,
    string? Caracter,
    string? Detalles,
    Guid Idgenero,
    Guid Idespecie,
    Guid Idraza,
    bool? Estaactivo
);

public record MascotaUpdateRecord(
    Guid Id,
    string Nombre,
    int Edad,
    string? Caracter,
    string? Detalles,
    Guid Idgenero,
    Guid Idespecie,
    Guid Idraza,
    bool? Estaactivo
);