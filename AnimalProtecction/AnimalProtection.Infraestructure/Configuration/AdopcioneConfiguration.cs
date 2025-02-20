using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

    public class AdopcioneConfiguration : IEntityTypeConfiguration<Adopcione>
    {
        public void Configure(EntityTypeBuilder<Adopcione> entity)
        {
            entity.HasKey(e => e.Id).HasName("adopciones_pkey");

            entity.ToTable("adopciones");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Estaactivo)
                .HasDefaultValue(true)
                .HasColumnName("estaactivo");
            entity.Property(e => e.Idmascota).HasColumnName("idmascota");
            entity.Property(e => e.Idtramite).HasColumnName("idtramite");

            entity.HasOne(d => d.IdmascotaNavigation).WithMany(p => p.Adopciones)
                .HasForeignKey(d => d.Idmascota)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_adopcionmascota");

            entity.HasOne(d => d.IdtramiteNavigation).WithMany(p => p.Adopciones)
                .HasForeignKey(d => d.Idtramite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tramiteadopcion");
        }
    }