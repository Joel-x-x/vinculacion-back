namespace AnimalProtection.Domain.Entities;

public partial class Rol
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Esadministrador { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Menusrol> MenusRoles { get; set; } = new List<Menusrol>();

    public virtual ICollection<UsuarioRol> UsuariosRoles { get; set; } = new List<UsuarioRol>();

}
