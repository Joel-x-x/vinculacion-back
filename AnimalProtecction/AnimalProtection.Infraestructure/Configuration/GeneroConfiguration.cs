using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class GeneroConfiguration:IEntityTypeConfiguration<Genero>
{
    public void Configure(EntityTypeBuilder<Genero> entity)
    {
        entity.HasKey(e => e.Id).HasName("generos_pkey");

        entity.ToTable("generos");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Nombre)
            .HasMaxLength(100)
            .HasColumnName("nombre");
    }

}