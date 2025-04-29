namespace AnimalProtection.Domain.Dto;

public record ArchivostramiteRecord(
    Guid Id,
    Guid Idarchivo,
    string? Descripcion,
    Guid Idtramite,
    bool? Estaactivo
    );