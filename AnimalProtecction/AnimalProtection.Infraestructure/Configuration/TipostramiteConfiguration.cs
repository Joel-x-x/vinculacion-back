using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class TipostramiteConfiguration: IEntityTypeConfiguration<Tipostramite>
{
    public void Configure(EntityTypeBuilder<Tipostramite> entity)
    {
        entity.HasKey(e => e.Id).HasName("tipostramite_pkey");

        entity.ToTable("tipostramite");

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