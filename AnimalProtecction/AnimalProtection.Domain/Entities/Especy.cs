﻿using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Domain.Entities;

public partial class Especy
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Estaactivo { get; set; }

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();

    public static Especy CreateFromRecord(EspecyCreateRecord especyCreateRecord)
    {
        return new Especy
        {
            Nombre = especyCreateRecord.Nombre,
            Estaactivo = true
        };
    }

    public void UpdateFromRecord(EspecyUpdateRecord especyUpdateRecord)
    {
        if (especyUpdateRecord.Nombre != null)
        {
            Nombre = especyUpdateRecord.Nombre;
        }
        if (especyUpdateRecord.Estaactivo != null)
        {
            Estaactivo = especyUpdateRecord.Estaactivo;
        }
    }

    public void Delete()
    {
        Estaactivo = false;
    }
}