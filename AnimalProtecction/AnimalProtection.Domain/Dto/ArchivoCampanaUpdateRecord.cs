namespace AnimalProtection.Domain.Dto;

public record ArchivoCampanaUpdateRecord(
    Guid Id,
    Guid IdArchivo,
    string Descripcion,
    Guid IdCampana,
    bool? EstaActivo
); 