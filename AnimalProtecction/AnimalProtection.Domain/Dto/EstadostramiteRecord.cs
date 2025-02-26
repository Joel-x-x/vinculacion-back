namespace AnimalProtection.Domain.Dto;

public record EstadostramiteRecord(
    Guid Id,
    string Nombre,
    int Orden,
    bool? Estaactivo
    );