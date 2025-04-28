using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class UsuarioConfiguration: IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> entity)
    {
        entity.HasKey(e => e.Id).HasName("usuarios_pkey");

        entity.ToTable("usuarios");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Apellido)
            .HasMaxLength(100)
            .HasColumnName("apellido");
        entity.Property(e => e.Contacto)
            .HasMaxLength(15)
            .HasColumnName("contacto");
        entity.Property(e => e.Email)
            .HasMaxLength(100)
            .HasColumnName("email");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Fechanacimiento).HasColumnName("fechanacimiento");
        entity.Property(e => e.Idarchivoperfil)
            .HasMaxLength(36)
            .IsFixedLength()
            .HasColumnName("idarchivoperfil");
        entity.Property(e => e.Identificacion)
            .HasMaxLength(20)
            .HasColumnName("identificacion");
        entity.Property(e => e.Nick)
            .HasMaxLength(20)
            .HasColumnName("nick");
        entity.Property(e => e.Nombre)
            .HasMaxLength(100)
            .HasColumnName("nombre");
        entity.Property(e => e.Pin)
            .HasMaxLength(4)
            .IsFixedLength()
            .HasColumnName("pin");
        entity.Property(e => e.Clave)
            .HasMaxLength(100)
            .HasColumnName("clave");
    }
}