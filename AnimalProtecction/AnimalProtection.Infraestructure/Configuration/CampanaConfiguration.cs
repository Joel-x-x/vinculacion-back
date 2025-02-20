using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class CampanaConfiguration: IEntityTypeConfiguration<Campana>
{
    public void Configure(EntityTypeBuilder<Campana> entity)
    {
        entity.HasKey(e => e.Id).HasName("campanas_pkey");

        entity.ToTable("campanas");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Cuerpo)
            .HasMaxLength(100)
            .HasColumnName("cuerpo");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Fechacaducidad)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("fechacaducidad");
        entity.Property(e => e.Fechaevento)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("fechaevento");
        entity.Property(e => e.Idtipocampana).HasColumnName("idtipocampana");
        entity.Property(e => e.Titulo)
            .HasMaxLength(200)
            .HasColumnName("titulo");

        entity.HasOne(d => d.IdtipocampanaNavigation).WithMany(p => p.Campanas)
            .HasForeignKey(d => d.Idtipocampana)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_tipocampana");
    }
}