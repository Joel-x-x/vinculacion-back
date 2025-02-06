using System;
using System.Collections.Generic;

namespace AnimalProtecction.Domain.Entities;

public partial class Menu
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Ruta { get; set; } = null!;

    public string? Icono { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Rolmenu> Rolmenus { get; set; } = new List<Rolmenu>();
}
