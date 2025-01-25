using System;
using System.Collections.Generic;

namespace AnimalProtecction.Domain.Entities;

public partial class Rol
{
    public Guid Id { get; set; }

    public string? Rol1 { get; set; }

    public bool? Estado { get; set; }

    public DateTime? Fechacreacion { get; set; }

    public DateTime? Fechamodificacion { get; set; }
}
