using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

    public class ArchivostramiteConfiguration : IEntityTypeConfiguration<Archivostramite>
    {
        public void Configure(EntityTypeBuilder<Archivostramite> entity)
        {
            entity.HasKey(e => e.Id).HasName("archivostramite_pkey");

            entity.ToTable("archivostramite");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estaactivo)
                .HasDefaultValue(true)
                .HasColumnName("estaactivo");
            entity.Property(e => e.Idarchivo).HasColumnName("idarchivo");
            entity.Property(e => e.Idtramite).HasColumnName("idtramite");

            entity.HasOne(d => d.IdtramiteNavigation).WithMany(p => p.Archivostramites)
                .HasForeignKey(d => d.Idtramite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_archivotramite");
        }
    }