namespace AnimalProtection.Domain.Entities;

public class Rol
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Esadministrador { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Menusrol> MenusRoles { get; set; } = new List<Menusrol>();

    public virtual ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();


    public static Rol CreateFromRecord(RolCreateRecord rolCreateRecord)
    {
        return new Rol
        {
            Id = Guid.NewGuid(),
            Nombre = rolCreateRecord.Nombre,
            Descripcion = rolCreateRecord.Descripcion,
            Esadministrador = rolCreateRecord.Esadministrador,
            Estaactivo = true
        };
    }

    public void UpdateFromRecord(RolUpdateRecord rolUpdateRecord)
    {
        Nombre = rolUpdateRecord.Nombre;
        Descripcion = rolUpdateRecord.Descripcion;
        Esadministrador = rolUpdateRecord.Esadministrador;
    }

    public void Delete()
    {
        Estaactivo = false;
    }

}
