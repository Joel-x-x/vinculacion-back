using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class MenuConfiguration:IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> entity)
    {
        entity.HasKey(e => e.Id).HasName("menus_pkey");

        entity.ToTable("menus");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Descripcion)
            .HasMaxLength(200)
            .HasColumnName("descripcion");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Link)
            .HasMaxLength(100)
            .HasColumnName("link");
        entity.Property(e => e.Nombre)
            .HasMaxLength(100)
            .HasColumnName("nombre");
    }
}