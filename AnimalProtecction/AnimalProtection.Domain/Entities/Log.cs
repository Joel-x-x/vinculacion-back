namespace AnimalProtection.Domain.Entities;

public partial class Log
{
    public Guid Id { get; set; }

    public string Usuario { get; set; } = null!;

    public DateTime? Fecha { get; set; }

    public string? Accion { get; set; }

    public string? Resultado { get; set; }

    public Guid Idusuario { get; set; }

    public Guid Idtipo { get; set; }

    public bool? Estaactivo { get; set; }

    public virtual Tiposlog IdtipoNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
