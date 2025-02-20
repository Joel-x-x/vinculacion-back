namespace AnimalProtection.Domain.Entities;

public partial class Hash
{
    public Guid Id { get; set; }

    public string? Clave { get; set; }

    public DateTime? Fechaactualizacion { get; set; }

    public Guid Idusuario { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
