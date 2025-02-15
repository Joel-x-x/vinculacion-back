using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class DatosInstitucionConfiguration : IEntityTypeConfiguration<DatosInstitucion>
{
    public void Configure(EntityTypeBuilder<DatosInstitucion> entity)
    {
        entity.ToTable("datosinstitucion");

        entity.HasKey(e => e.Id).HasName("datosinstitucion_pkey");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");

        entity.Property(e => e.Nombre)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("nombre");

        entity.Property(e => e.ColorPrincipal)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnName("colorprincipal");

        entity.Property(e => e.ColorSecundario)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnName("colorsecundario");

        entity.Property(e => e.Ubicacion)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("ubicacion");

        entity.Property(e => e.QuienesSomos)
            .HasMaxLength(300)
            .IsRequired()
            .HasColumnName("quienessomos");

        entity.Property(e => e.Mision)
            .HasMaxLength(300)
            .IsRequired()
            .HasColumnName("mision");

        entity.Property(e => e.Vision)
            .HasMaxLength(300)
            .IsRequired()
            .HasColumnName("vision");

        entity.Property(e => e.IdArchivoLogo)
            .HasColumnName("idarchivologo");

        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
    }
}