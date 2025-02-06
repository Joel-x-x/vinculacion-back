using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.Id).HasName("usuario_pkey");
            builder.ToTable("usuario");

            builder.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");

            builder.Property(e => e.Apellido)
                .HasMaxLength(255)
                .HasColumnName("apellido");

            builder.Property(e => e.Contacto)
                .HasMaxLength(10)
                .HasColumnName("contacto");

            builder.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");

            builder.Property(e => e.Estado)
                .HasColumnName("estado");

            builder.Property(e => e.Fechaactualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaactualizacion");

            builder.Property(e => e.Fechacreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechacreacion");

            builder.Property(e => e.Fechanacimiento)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechanacimiento");

            builder.Property(e => e.Foto)
                .HasMaxLength(255)
                .HasColumnName("foto");

            builder.Property(e => e.Identificacion)
                .HasMaxLength(10)
                .HasColumnName("identificacion");

            builder.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        }
    }