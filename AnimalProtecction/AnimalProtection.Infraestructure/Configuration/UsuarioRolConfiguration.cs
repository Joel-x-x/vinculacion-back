using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class UsuarioRolConfiguration: IEntityTypeConfiguration<UsuarioRol>
{
    public void Configure(EntityTypeBuilder<UsuarioRol> entity)
    {
        entity.HasKey(e => e.Id).HasName("usuario_rol_pkey");

        entity.ToTable("usuario_rol");

        entity.HasIndex(e => new { e.UsuarioId, e.RolId }, "usuario_rol_usuario_id_rol_id_key").IsUnique();

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.FechaAsignacion)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("fecha_asignacion");
        entity.Property(e => e.RolId).HasColumnName("rol_id");
        entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
    }

}