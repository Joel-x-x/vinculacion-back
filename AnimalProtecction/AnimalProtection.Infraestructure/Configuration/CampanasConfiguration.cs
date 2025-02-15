using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class CampanasConfiguration : IEntityTypeConfiguration<Campanas>
{
    public void Configure(EntityTypeBuilder<Campanas> entity)
    {
        entity.ToTable("campanas");

        entity.HasKey(e => e.Id).HasName("campanas_pkey");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");

        entity.Property(e => e.Titulo)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("titulo");

        entity.Property(e => e.Cuerpo)
            .HasMaxLength(100)
            .IsRequired()
            .HasColumnName("cuerpo");

        entity.Property(e => e.FechaEvento)
            .HasColumnName("fechaevento");

        entity.Property(e => e.FechaCaducidad)
            .HasColumnName("fechacaducidad");

        entity.Property(e => e.IdTipoCampana)
            .IsRequired()
            .HasColumnName("idtipocampana");

        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");

        // Relación con TiposCampana
        entity.HasOne(e => e.TipoCampana)
            .WithMany()
            .HasForeignKey(e => e.IdTipoCampana)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_tipocampana");
    }
}