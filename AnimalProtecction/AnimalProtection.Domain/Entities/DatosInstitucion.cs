namespace AnimalProtection.Domain.Entities;
public class DatosInstitucion
{
    public Guid Id { get; set; }
    
    public string Nombre { get; set; }
    
    public string Ubicacion { get; set; }
    
    public string QuienesSomos { get; set; }
    
    public string Mision { get; set; }
    
    public string Vision { get; set; }
    
    public string ColorPrincipal { get; set; }

    public string ColorSecundario { get; set; }

    public Guid? IdArchivoLogo { get; set; }
    
    public bool EstaActivo { get; set; }


    // Virtuals
    public virtual ICollection<RedesSociales> ReedesSociales { get; set; }


    // Crear el método para agregar nueva institución

    public static DatosInstitucion CreateFromRecord(DatosInstitucionCreateRecord datosInstitucionCreateRecord)
    {
        return new DatosInstitucion
        {
            Id = Guid.NewGuid(),
            Nombre = datosInstitucionCreateRecord.Nombre,
            Ubicacion = datosInstitucionCreateRecord.Ubicacion,
            QuienesSomos = datosInstitucionCreateRecord.QuienesSomos,
            Mision = datosInstitucionCreateRecord.Mision,
            Vision = datosInstitucionCreateRecord.Vision,
            ColorPrincipal = datosInstitucionCreateRecord.ColorPrincipal,
            ColorSecundario = datosInstitucionCreateRecord.ColorSecundario,
            IdArchivoLogo = datosInstitucionCreateRecord.IdArchivoLogo,
            EstaActivo = true
        };
    }

    // Crear el método para actualizar institución

    public void UpdateFromRecord(DatosInstitucionUpdateRecord datosInstitucionUpdateRecord)
    {
        Nombre = datosInstitucionUpdateRecord.Nombre;
        Ubicacion = datosInstitucionUpdateRecord.Ubicacion;
        QuienesSomos = datosInstitucionUpdateRecord.QuienesSomos;
        Mision = datosInstitucionUpdateRecord.Mision;
        Vision = datosInstitucionUpdateRecord.Vision;
        ColorPrincipal = datosInstitucionUpdateRecord.ColorPrincipal;
        ColorSecundario = datosInstitucionUpdateRecord.ColorSecundario;
        IdArchivoLogo = datosInstitucionUpdateRecord.IdArchivoLogo;
    }

    // Crear el método para eliminar institución

    public void Delete()
    {
        EstaActivo = false;
    }

}
