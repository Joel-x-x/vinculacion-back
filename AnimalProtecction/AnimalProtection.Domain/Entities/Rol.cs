namespace AnimalProtection.Domain.Entities;

public partial class Rol
{
    public Guid Id { get; set; }

    public string? Rol1 { get; set; }

    public bool? Estado { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }
    
    public virtual ICollection<Rolmenu> Rolmenus { get; set; } = new List<Rolmenu>();
    public virtual ICollection<RoleUsuario> RoleUsuarios { get; set; } = new List<RoleUsuario>();


}
