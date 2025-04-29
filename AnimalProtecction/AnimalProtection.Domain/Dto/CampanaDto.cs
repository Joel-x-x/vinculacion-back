using System;

namespace AnimalProtection.Domain.Dto;

public record CampanaRecord(
    Guid Id,
    string Titulo,
    string Tipo,
    string Cuerpo,
    DateTime? FechaEvento,
    DateTime? FechaCaducidad,
    Guid IdTipoCampana,
    bool EstaActivo
);

public record CampanaCreateRecord(
    string Titulo,
    string Cuerpo,
    DateTime? FechaEvento,
    DateTime? FechaCaducidad,
    Guid IdTipoCampana
);

public record CampanaUpdateRecord(
    string Titulo,
    string Cuerpo,
    DateTime? FechaEvento,
    DateTime? FechaCaducidad,
    Guid IdTipoCampana,
    bool EstaActivo
); 