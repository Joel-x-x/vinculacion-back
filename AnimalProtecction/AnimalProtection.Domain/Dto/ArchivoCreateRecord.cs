namespace AnimalProtection.Domain.Dto
{
    public record ArchivoCreateRecord(
        Guid? Id,
        string Url,
        string Formato,
        Guid Idtipoarchivo,
        bool? Estaactivo
    );
}
