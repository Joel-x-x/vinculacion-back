using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto{

    public record RolRecord{
        Guid Id,
        string Nombre,
        string? Descripcion,
        bool? Esadministrador,
        bool? Estaactivo
    }
    {
        public RolRecord(Rol rol) : this(
            rol.Id,
            rol.Nombre,
            rol.Descripcion,
            rol.Esadministrador,
            rol.Estaactivo
        )
        { }

    }

}

public record RolCreateRecord(
    string Nombre,
    string? Descripcion,
    bool? Esadministrador
);

public record RolUpdateRecord(
    Guid Id,
    string Nombre,
    string? Descripcion,
    bool? Esadministrador
);