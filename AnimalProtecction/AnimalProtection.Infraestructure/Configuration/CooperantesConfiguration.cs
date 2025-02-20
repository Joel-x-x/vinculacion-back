using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class CooperantesConfiguration : IEntityTypeConfiguration<Cooperantes>
{
    public void Configure(EntityTypeBuilder<Cooperantes> entity)
    {
        entity.HasKey(e => e.Id).HasName("cooperantes_pkey");

        entity.ToTable("cooperantes");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");

        entity.Property(e => e.Nombre)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("nombre");

        entity.Property(e => e.Descricpion)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("descripcion");

        entity.Property(e => e.ColorSecundario)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnName("colorsecundario");

        entity.Property(e => e.IdArchivoLogo)
            .HasColumnName("idarchivologo");

        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
    }
}