using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class DatosinstitucionConfiguration : IEntityTypeConfiguration<DatosInstitucion>
{
    public void Configure(EntityTypeBuilder<DatosInstitucion> entity)
    {
        entity.HasKey(e => e.Id).HasName("datosinstitucion_pkey");

        entity.ToTable("datosinstitucion");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.ColorPrincipal)
            .HasMaxLength(50)
            .HasColumnName("colorprincipal");
        entity.Property(e => e.ColorSecundario)
            .HasMaxLength(50)
            .HasColumnName("colorsecundario");
        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.IdArchivoLogo).HasColumnName("idarchivologo");
        entity.Property(e => e.Mision)
            .HasMaxLength(300)
            .HasColumnName("mision");
        entity.Property(e => e.Nombre)
            .HasMaxLength(200)
            .HasColumnName("nombre");
        entity.Property(e => e.QuienesSomos)
            .HasMaxLength(300)
            .HasColumnName("quienessomos");
        entity.Property(e => e.Ubicacion)
            .HasMaxLength(200)
            .HasColumnName("ubicacion");
        entity.Property(e => e.Vision)
            .HasMaxLength(300)
            .HasColumnName("vision");
    }

}