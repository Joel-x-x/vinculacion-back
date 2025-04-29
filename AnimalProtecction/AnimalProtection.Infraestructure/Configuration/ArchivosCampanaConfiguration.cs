using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

    public class ArchivoscampanaConfiguration : IEntityTypeConfiguration<Archivoscampana>
    { 
        public void Configure(EntityTypeBuilder<Archivoscampana> entity)
        {
            entity.HasKey(e => e.Id).HasName("archivoscampana_pkey");

            entity.ToTable("archivoscampana");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estaactivo)
                .HasDefaultValue(true)
                .HasColumnName("estaactivo");
            entity.Property(e => e.Idarchivo).HasColumnName("idarchivo");
            entity.Property(e => e.Idcampana).HasColumnName("idcampana");

            entity.HasOne(d => d.IdcampanaNavigation).WithMany(p => p.Archivoscampanas)
                .HasForeignKey(d => d.Idcampana)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_archivocampana");
        }
    }