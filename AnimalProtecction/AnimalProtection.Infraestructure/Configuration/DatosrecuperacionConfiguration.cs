using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class DatosrecuperacionConfiguration : IEntityTypeConfiguration<Datosrecuperacion>
{
    public void Configure(EntityTypeBuilder<Datosrecuperacion> entity)
    {
        entity.HasKey(e => e.Id).HasName("datosrecuperacion_pkey");

        entity.ToTable("datosrecuperacion");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Idusuario).HasColumnName("idusuario");
        entity.Property(e => e.Pregunta)
            .HasMaxLength(100)
            .HasColumnName("pregunta");
        entity.Property(e => e.Respuesta)
            .HasMaxLength(100)
            .HasColumnName("respuesta");

        entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Datosrecuperacions)
            .HasForeignKey(d => d.Idusuario)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_recuperacionusuario");
    }

}