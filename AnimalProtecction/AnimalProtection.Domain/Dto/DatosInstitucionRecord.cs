using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto
{
    public record DatosInstitucionRecord(
        Guid Id,
        string Nombre,
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
    string Nombre,
    string Ubicacion,
    string QuienesSomos,
    string Mision,
    string Vision,
    string ColorPrincipal,
    string ColorSecundario,
    Guid? IdArchivoLogo
);

public record DatosInstitucionUpdateRecord(
    Guid Id,
    string Nombre,
    string Ubicacion,
    string QuienesSomos,
    string Mision,
    string Vision,
    string ColorPrincipal,
    string ColorSecundario,
    Guid? IdArchivoLogo
);