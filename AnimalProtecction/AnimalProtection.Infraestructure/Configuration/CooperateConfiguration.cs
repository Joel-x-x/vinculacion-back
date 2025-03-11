using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class CooperateConfiguration: IEntityTypeConfiguration<Cooperantes>
{
    public void Configure(EntityTypeBuilder<Cooperantes> entity)
    {
        entity.HasKey(e => e.Id).HasName("cooperantes_pkey");

        entity.ToTable("cooperantes");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.ColorSecundario)
            .HasMaxLength(50)
            .HasColumnName("colorsecundario");
        entity.Property(e => e.Descripcion)
            .HasMaxLength(200)
            .HasColumnName("descripcion");
        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.IdArchivoLogo).HasColumnName("idarchivologo");
        entity.Property(e => e.Nombre)
            .HasMaxLength(200)
            .HasColumnName("nombre");
    }
}