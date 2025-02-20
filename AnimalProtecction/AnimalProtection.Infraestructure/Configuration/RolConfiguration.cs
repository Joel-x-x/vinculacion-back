using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtection.Configuration;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> entity)
    {
        entity.HasKey(e => e.Id).HasName("roles_pkey");

        entity.ToTable("roles");

        entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");

        entity.Property(e => e.Nombre)
            .IsRequired()
            .HasColumnName("nombre");
        
        entity.Property(e => e.Descripcion)
            .HasColumnName("descripcion");
        
        entity.Property(e => e.Esadministrador)
            .HasColumnName("esadministrador");
        
        entity.Property(e => e.Estaactivo)
            .HasColumnName("estaactivo");
    }
}