namespace AnimalProtection.Domain.Dto;

public record TiposCampanaRecord(
    Guid Id,
    string Nombre,
    bool? EstaActivo
); 