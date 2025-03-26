namespace AnimalProtection.Domain.Dto
{
    public record ArchivoCreateRecord(
        string Url,
        string Formato,
        Guid Idtipoarchivo
    );
}
