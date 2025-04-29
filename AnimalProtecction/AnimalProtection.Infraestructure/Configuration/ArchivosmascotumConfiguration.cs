using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

    public class ArchivosmascotumConfiguration : IEntityTypeConfiguration<Archivosmascotum>
    {
        public void Configure(EntityTypeBuilder<Archivosmascotum> entity)
        {
            entity.HasKey(e => e.Id).HasName("archivosmascota_pkey");

            entity.ToTable("archivosmascota");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Esprincipal)
                .HasDefaultValue(false)
                .HasColumnName("esprincipal");
            entity.Property(e => e.Estaactivo)
                .HasDefaultValue(true)
                .HasColumnName("estaactivo");
            entity.Property(e => e.Idarchivo).HasColumnName("idarchivo");
            entity.Property(e => e.Idmascota).HasColumnName("idmascota");
            entity.Property(e => e.Orden).HasColumnName("orden");

            entity.HasOne(d => d.IdmascotaNavigation).WithMany(p => p.Archivosmascota)
                .HasForeignKey(d => d.Idmascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_archivomascota");
        }
    }