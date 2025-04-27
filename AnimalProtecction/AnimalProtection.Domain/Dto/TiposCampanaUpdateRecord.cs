namespace AnimalProtection.Domain.Dto;

public record TiposCampanaUpdateRecord(
    Guid Id,
    string Nombre,
    bool? EstaActivo
); 