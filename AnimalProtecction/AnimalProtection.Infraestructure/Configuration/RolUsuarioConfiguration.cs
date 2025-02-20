using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

    public class RolUsuarioConfiguration : IEntityTypeConfiguration<RoleUsuario>
    {
        public void Configure(EntityTypeBuilder<RoleUsuario> entity)
        {
            
            entity.HasKey(e => e.Id).HasName("role_usuario_pkey");

            entity.ToTable("role_usuario");

            entity.HasIndex(e => new { e.UsuarioId, e.RolId }, "idx_usuario_rol_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Rol).WithMany(p => p.RoleUsuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("fk_rol");

            // entity.HasOne(d => d.Usuario)
            //     .WithMany(p => p.RoleUsuarios)
            //     .HasForeignKey(d => d.UsuarioId)
            //     .HasConstraintName("fk_usuario");
        }
    }