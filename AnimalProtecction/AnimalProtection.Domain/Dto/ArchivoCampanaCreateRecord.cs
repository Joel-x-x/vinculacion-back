namespace AnimalProtection.Domain.Dto;

public record ArchivoCampanaCreateRecord(
    Guid IdArchivo,
    string Descripcion,
    Guid IdCampana
); 