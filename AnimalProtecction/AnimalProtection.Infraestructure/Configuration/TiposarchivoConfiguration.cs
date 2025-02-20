using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class TiposarchivoConfiguration: IEntityTypeConfiguration<Tiposarchivo>
{
    public void Configure(EntityTypeBuilder<Tiposarchivo> entity)
    {
        entity.HasKey(e => e.Id).HasName("tiposarchivo_pkey");

        entity.ToTable("tiposarchivo");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Descripcion)
            .HasMaxLength(200)
            .HasColumnName("descripcion");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Nombre)
            .HasMaxLength(100)
            .HasColumnName("nombre");  
    }
}