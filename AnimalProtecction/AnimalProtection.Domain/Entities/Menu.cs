using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Domain.Entities;

public partial class Menu
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Link { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Menusrol> Menusrols { get; set; } = new List<Menusrol>();

    //Crear el metodo para generar el menu
    public static Menu CreateFromRecord(MenuCreateRecord menuCreateRecord)
    {
        return new Menu
        {
            Id = Guid.NewGuid(),
            Nombre = menuCreateRecord.Nombre,
            Descripcion = menuCreateRecord.Descripcion,
            Link = menuCreateRecord.Link,
            Estaactivo = true
        };
    }

    public void UpdateFromRecord(MenuUpdateRecord menuUpdateRecord)
    {
        if (!string.Equals(Nombre, menuUpdateRecord.Nombre, StringComparison.Ordinal))
            Nombre = menuUpdateRecord.Nombre;
        if (!string.Equals(Descripcion, menuUpdateRecord.Descripcion, StringComparison.Ordinal))
            Descripcion = menuUpdateRecord.Descripcion;
        if (!string.Equals(Link, menuUpdateRecord.Link, StringComparison.Ordinal))
            Link = menuUpdateRecord.Link;
    }

    public void eliminar()
    {
        Estaactivo = false;
    }

}
