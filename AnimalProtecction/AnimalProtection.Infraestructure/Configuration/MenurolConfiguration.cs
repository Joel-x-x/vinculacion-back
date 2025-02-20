using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class MenurolConfiguration :IEntityTypeConfiguration<Menusrol>
{
    public void Configure(EntityTypeBuilder<Menusrol> entity)
    {
        entity.HasKey(e => e.Id).HasName("menusrol_pkey");

        entity.ToTable("menusrol");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Idmenu).HasColumnName("idmenu");
        entity.Property(e => e.Idrol).HasColumnName("idrol");

        entity.HasOne(d => d.IdmenuNavigation).WithMany(p => p.Menusrols)
            .HasForeignKey(d => d.Idmenu)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_menurol");

        entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Menusrols)
            .HasForeignKey(d => d.Idrol)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_rolmenu");
    }

}