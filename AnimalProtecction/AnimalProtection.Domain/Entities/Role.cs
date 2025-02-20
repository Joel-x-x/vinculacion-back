namespace AnimalProtection.Domain.Entities;

public partial class Role
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Esadministrador { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Menusrol> Menusrols { get; set; } = new List<Menusrol>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public virtual ICollection<UsuarioRol> RolesUsuario { get; set; } = new List<UsuarioRol>();

}
