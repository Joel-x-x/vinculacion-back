using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Domain.Entities;

public partial class Cooperantes
{
    public Guid Id { get; set; }

    public string Nombre { get; set; }

    public string Descripcion { get; set; }

    public string ColorSecundario { get; set; }

    public Guid? IdArchivoLogo { get; set; }

    public bool EstaActivo { get; set; }

    //Crear el m�todo para generar el cooperante

    public static Cooperantes CreateFromRecord(CooperantesCreateRecord cooperantesCreateRecors)
    {
        return new Cooperantes
        {
            Id = Guid.NewGuid(),
            Nombre = cooperantesCreateRecors.Nombre,
            Descripcion = cooperantesCreateRecors.Descripcion,
            ColorSecundario = cooperantesCreateRecors.ColorSecundario,
            IdArchivoLogo = cooperantesCreateRecors.IdArchivoLogo,
            EstaActivo = true
        };
    }

    public void UpdateFromRecord(CooperantesUpdateRecord cooperantesUpdateRecord)
    {
        if (!string.Equals(Nombre, cooperantesUpdateRecord.Nombre, StringComparison.Ordinal))
            Nombre = cooperantesUpdateRecord.Nombre;
        if (!string.Equals(Descripcion, cooperantesUpdateRecord.Descripcion, StringComparison.Ordinal))
            Descripcion = cooperantesUpdateRecord.Descripcion;
        if (!string.Equals(ColorSecundario, cooperantesUpdateRecord.ColorSecundario, StringComparison.Ordinal))
            ColorSecundario = cooperantesUpdateRecord.ColorSecundario;
        if (cooperantesUpdateRecord.IdArchivoLogo.HasValue && IdArchivoLogo != cooperantesUpdateRecord.IdArchivoLogo.Value)
            IdArchivoLogo = cooperantesUpdateRecord.IdArchivoLogo.Value;
    }

    public void eliminar()
    {
        EstaActivo = false;
    }
}