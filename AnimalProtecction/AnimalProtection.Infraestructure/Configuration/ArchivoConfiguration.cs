using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

    public class ArchivoConfiguration : IEntityTypeConfiguration<Archivo>
    {
        public void Configure(EntityTypeBuilder<Archivo> entity)
        {
            entity.HasKey(e => e.Id).HasName("archivos_pkey");

            entity.ToTable("archivos");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Estaactivo)
                .HasDefaultValue(true)
                .HasColumnName("estaactivo");
            entity.Property(e => e.Formato)
                .HasMaxLength(10)
                .HasColumnName("formato");
            entity.Property(e => e.Idtipoarchivo).HasColumnName("idtipoarchivo");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .HasColumnName("url");

            entity.HasOne(d => d.IdtipoarchivoNavigation).WithMany(p => p.Archivos)
                .HasForeignKey(d => d.Idtipoarchivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tipoarchivo");
        }
    }