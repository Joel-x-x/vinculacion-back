using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class TiposCampanaConfiguration : IEntityTypeConfiguration<TiposCampana>
{
    public void Configure(EntityTypeBuilder<TiposCampana> entity)
    {
        entity.ToTable("tiposcampana");

        entity.HasKey(e => e.Id).HasName("tiposcampana_pkey");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");

        entity.Property(e => e.Nombre)
            .HasMaxLength(100)
            .IsRequired()
            .HasColumnName("nombre");

        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
    }
}