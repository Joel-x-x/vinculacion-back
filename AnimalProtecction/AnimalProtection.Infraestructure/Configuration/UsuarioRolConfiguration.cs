using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtection.Configuration;

public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
{
    public void Configure(EntityTypeBuilder<UsuarioRol> entity)
    {
        entity.HasKey(e => e.Id).HasName("usuariosrol_pkey");

        entity.ToTable("usuariosrol");

        entity.HasIndex(e => new { e.Idusuario, e.Idrol }, "idx_usuario_rol_unique").IsUnique();

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");

        entity.Property(e => e.Fechaasignacion)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("fechaasignacion");

        entity.Property(e => e.Idrol).HasColumnName("idrol");
        entity.Property(e => e.Idusuario).HasColumnName("idusuario");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");

        entity.HasOne(d => d.Rol)
            .WithMany(p => p.UsuariosRoles)
            .HasForeignKey(d => d.Idrol)
            .HasConstraintName("fk_rolusuariosrol");

        entity.HasOne(d => d.Usuario)
            .WithMany(p => p.UsuariosRoles)
            .HasForeignKey(d => d.Idusuario)
            .HasConstraintName("fk_usuariousuariosrol");
    }
}