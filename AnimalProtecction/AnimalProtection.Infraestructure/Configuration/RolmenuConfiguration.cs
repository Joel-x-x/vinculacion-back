using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

    public class RolmenuConfiguration : IEntityTypeConfiguration<Rolmenu>
    {
        public void Configure(EntityTypeBuilder<Rolmenu> entity)
        {
            entity.HasKey(e => e.Id).HasName("rolmenu_pkey");

            entity.ToTable("rolmenu");

            entity.HasIndex(e => new { e.RolId, e.MenuId }, "idx_rol_menu_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.RolId).HasColumnName("rol_id");

            // entity.HasOne(d => d.Menu).WithMany(p => p.Rolmenus)
            //     .HasForeignKey(d => d.MenuId)
            //     .HasConstraintName("fk_menu");

            entity.HasOne(d => d.Rol)
                .WithMany(p => p.Rolmenus)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("fk_rol");
            
            
            
        }
    }