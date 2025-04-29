namespace AnimalProtection.Domain.Entities;

public partial class Menusrol
{
    public Guid Id { get; set; }

    public Guid Idrol { get; set; }

    public Guid Idmenu { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Menu IdmenuNavigation { get; set; } = null!;

    public virtual Rol IdrolNavigation { get; set; } = null!;
}
