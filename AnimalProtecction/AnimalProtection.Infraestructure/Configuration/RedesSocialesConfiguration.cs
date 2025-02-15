using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class RedesSocialesConfiguration : IEntityTypeConfiguration<RedesSociales>
{
    public void Configure(EntityTypeBuilder<RedesSociales> entity)
    {
        entity.ToTable("redessociales");

        entity.HasKey(e => e.Id).HasName("redessociales_pkey");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");

        entity.Property(e => e.Nombre)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("nombre");

        entity.Property(e => e.UrlRedSocial)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("urlredsocial");

        entity.Property(e => e.IdArchivoLogo)
            .HasColumnName("idarchivologo");

        entity.Property(e => e.IdInstitucion)
            .IsRequired()
            .HasColumnName("idinstitucion");

        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");

        // Relación con DatosInstitucion
        entity.HasOne(e => e.DatosInstitucion)
            .WithMany()  // Si en `DatosInstitucion` tienes una lista `public List<RedesSociales> RedesSociales`, aquí pon `.WithMany(i => i.RedesSociales)`
            .HasForeignKey(e => e.IdInstitucion)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_institucion");
    }
}