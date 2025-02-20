using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class MascotaConfiguration:IEntityTypeConfiguration<Mascota>
{
    public void Configure(EntityTypeBuilder<Mascota> entity)
    {
        entity.HasKey(e => e.Id).HasName("mascotas_pkey");

        entity.ToTable("mascotas");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Caracter)
            .HasMaxLength(200)
            .HasColumnName("caracter");
        entity.Property(e => e.Detalles)
            .HasMaxLength(200)
            .HasColumnName("detalles");
        entity.Property(e => e.Edad).HasColumnName("edad");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Idespecie).HasColumnName("idespecie");
        entity.Property(e => e.Idgenero).HasColumnName("idgenero");
        entity.Property(e => e.Idraza).HasColumnName("idraza");
        entity.Property(e => e.Nombre)
            .HasMaxLength(100)
            .HasColumnName("nombre");

        entity.HasOne(d => d.IdespecieNavigation).WithMany(p => p.Mascota)
            .HasForeignKey(d => d.Idespecie)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_especiemascota");

        entity.HasOne(d => d.IdgeneroNavigation).WithMany(p => p.Mascota)
            .HasForeignKey(d => d.Idgenero)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_generomascota");

        entity.HasOne(d => d.IdrazaNavigation).WithMany(p => p.Mascota)
            .HasForeignKey(d => d.Idraza)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_razamascota");
    }
}