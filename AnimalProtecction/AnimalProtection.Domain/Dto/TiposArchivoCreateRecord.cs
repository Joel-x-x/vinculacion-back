namespace AnimalProtection.Domain.Dto
{
    public record TiposArchivoCreateRecord(
        Guid? Id,
        string Nombre,
        string? Descripcion,
        bool? Estaactivo
    );
}
