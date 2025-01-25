using System;
using System.Collections.Generic;

namespace AnimalProtecction.Domain.Entities;

public partial class Usuario
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateTime Fechanacimiento { get; set; }

    public string Identificacion { get; set; } = null!;

    public string Contacto { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Estado { get; set; }

    public string? Foto { get; set; }

    public DateTime Fechacreacion { get; set; }

    public DateTime? Fechaactualizacion { get; set; }
}
