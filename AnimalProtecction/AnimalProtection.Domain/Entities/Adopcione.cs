using AnimalProtection.Domain.Dto;


namespace AnimalProtection.Domain.Entities;

public partial class Adopcione
{
    public Guid Id { get; set; }

    public Guid Idmascota { get; set; }

    public Guid Idtramite { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Mascota IdmascotaNavigation { get; set; } = null!;

    public virtual Tramite IdtramiteNavigation { get; set; } = null!;

    public static Adopcione CreateFromRecord(AdopcionesCreateRecord adopcionesCreateRecors)
    {
        return new Adopcione
        {
            Id = Guid.NewGuid(),
            Idmascota = adopcionesCreateRecors.Idmascota,
            Idtramite = adopcionesCreateRecors.Idtramite,
            Estaactivo = true
        };
    }

    public void UpdateFromRecord(AdopcionesUpdateRecord adopcionesUpdateRecord)
    {
        if (adopcionesUpdateRecord.Idmascota.HasValue && Idmascota != adopcionesUpdateRecord.Idmascota.Value)
            Idmascota = adopcionesUpdateRecord.Idmascota.Value;

        if (adopcionesUpdateRecord.Idtramite.HasValue && Idtramite != adopcionesUpdateRecord.Idtramite.Value)
            Idtramite = adopcionesUpdateRecord.Idtramite.Value;
    }

    public void eliminar()
    {
        Estaactivo = false;
    }
}
