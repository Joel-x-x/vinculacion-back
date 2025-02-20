using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class LogConfiguration:IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> entity)
    {
        entity.HasKey(e => e.Id).HasName("logs_pkey");

        entity.ToTable("logs");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Accion).HasColumnName("accion");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Fecha)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("fecha");
        entity.Property(e => e.Idtipo).HasColumnName("idtipo");
        entity.Property(e => e.Idusuario).HasColumnName("idusuario");
        entity.Property(e => e.Resultado).HasColumnName("resultado");
        entity.Property(e => e.Usuario)
            .HasMaxLength(100)
            .HasColumnName("usuario");

        entity.HasOne(d => d.IdtipoNavigation).WithMany(p => p.Logs)
            .HasForeignKey(d => d.Idtipo)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_tipolog");

        entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Logs)
            .HasForeignKey(d => d.Idusuario)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_logusuario");
    }
}