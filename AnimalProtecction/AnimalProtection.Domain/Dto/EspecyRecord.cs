using AnimalProtection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalProtection.Domain.Dto;

public record EspecyRecord(
    Guid Id,
    string Nombre,
    bool? Estaactivo
    )
{
    public EspecyRecord(Especy especy) : this(
        especy.Id,
        especy.Nombre,
        especy.Estaactivo
    )
    { }
}

public record EspecyCreateRecord(
    Guid? Id,
    string Nombre,
    bool? Estaactivo
);

public record EspecyUpdateRecord(
    Guid Id,
    string Nombre,
    bool? Estaactivo
);