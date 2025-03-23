namespace AnimalProtection.Domain.Dto
{
    public record ArchivoUpdateRecord(
        Guid Id,
        string Url,
        string Formato,
        Guid Idtipoarchivo,
        bool? Estaactivo
    );
}
