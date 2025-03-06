using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto
{
    public record DatosInstitucionRecord(
        Guid Id,
        string Nombre,
        string ColorPagina,
        string UrlLogo,
        string Ubicacion,
        string QuienesSomos,
        string Mision,
        string Vision,
        string ColorPrincipal,
        string ColorSecundario,
        Guid? IdArchivoLogo,
        bool EstaActivo
    )
    {
        public DatosInstitucionRecord(DatosInstitucion datosInstitucion) : this(
            datosInstitucion.Id,
            datosInstitucion.Nombre,
            datosInstitucion.ColorPagina,
            datosInstitucion.UrlLogo,
            datosInstitucion.Ubicacion,
            datosInstitucion.QuienesSomos,
            datosInstitucion.Mision,
            datosInstitucion.Vision,
            datosInstitucion.ColorPrincipal,
            datosInstitucion.ColorSecundario,
            datosInstitucion.IdArchivoLogo,
            datosInstitucion.EstaActivo
        )
        { }
    }
}

public record DatosInstitucionCreateRecord(
    Guid? Id,
    string Nombre,
    string ColorPagina,
    string UrlLogo,
    string Ubicacion,
    string QuienesSomos,
    string Mision,
    string Vision,
    string ColorPrincipal,
    string ColorSecundario,
    Guid? IdArchivoLogo,
    bool EstaActivo
);

public record DatosInstitucionUpdateRecord(
    Guid Id,
    string Nombre,
    string ColorPagina,
    string UrlLogo,
    string Ubicacion,
    string QuienesSomos,
    string Mision,
    string Vision,
    string ColorPrincipal,
    string ColorSecundario,
    Guid? IdArchivoLogo,
    bool EstaActivo
);