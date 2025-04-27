namespace AnimalProtection.Domain.Dto;

public record ArchivoCampanaRecord(
    Guid Id,
    Guid IdArchivo,
    string Descripcion,
    Guid IdCampana,
    bool? EstaActivo,
    ArchivoRecord? Archivo
); 