using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record MenuRecord(
    Guid Id,
    string Nombre,
    string Descripcion,
    string Link,
    bool? Estaactivo

){
    public MenuRecord(Menu menu) : this (
        menu.Id,
        menu.Nombre,
        menu.Descripcion,
        menu.Link,
        menu.Estaactivo
    )
    {}
}

public record MenuCreateRecord(
    string Nombre,
    string Descripcion,
    string Link
);

public record MenuUpdateRecord(
    Guid Id,
    string Nombre,
    string Descripcion,
    string Link,
    bool Estaactivo
);