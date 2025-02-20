using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class EstadostramiteConfiguration :IEntityTypeConfiguration<Estadostramite>
{
    public void Configure(EntityTypeBuilder<Estadostramite> entity)
    {
        entity.HasKey(e => e.Id).HasName("estadostramite_pkey");

        entity.ToTable("estadostramite");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Nombre)
            .HasMaxLength(100)
            .HasColumnName("nombre");
        entity.Property(e => e.Orden)
            .HasDefaultValue(1)
            .HasColumnName("orden");
    }
}