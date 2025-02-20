using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class ArchivosCampanaConfiguration : IEntityTypeConfiguration<ArchivosCampana>
{
    public void Configure(EntityTypeBuilder<ArchivosCampana> entity)
    {
        entity.ToTable("archivoscampana");

        entity.HasKey(e => e.Id).HasName("archivoscampana_pkey");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");

        entity.Property(e => e.IdArchivo)
            .IsRequired()
            .HasColumnName("idarchivo");

        entity.Property(e => e.Descripcion)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("descripcion");

        entity.Property(e => e.IdCampana)
            .IsRequired()
            .HasColumnName("idcampana");

        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");

        // Relación con Campanas
        entity.HasOne(e => e.Campana)
            .WithMany()
            .HasForeignKey(e => e.IdCampana)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_archivocampana");
    }
}