using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class TramiteConfiguration: IEntityTypeConfiguration<Tramite>
{
    public void Configure(EntityTypeBuilder<Tramite> entity)
    {
        entity.HasKey(e => e.Id).HasName("tramites_pkey");

            entity.ToTable("tramites");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Contacto)
                .HasMaxLength(15)
                .HasColumnName("contacto");
            entity.Property(e => e.Datos)
                .HasMaxLength(500)
                .HasColumnName("datos");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Estaactivo)
                .HasDefaultValue(true)
                .HasColumnName("estaactivo");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.Idestadotramite).HasColumnName("idestadotramite");
            entity.Property(e => e.Idtipotramite).HasColumnName("idtipotramite");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Numerotramite).HasColumnName("numerotramite");

            entity.HasOne(d => d.IdestadotramiteNavigation).WithMany(p => p.Tramites)
                .HasForeignKey(d => d.Idestadotramite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estadotramite");

            entity.HasOne(d => d.IdtipotramiteNavigation).WithMany(p => p.Tramites)
                .HasForeignKey(d => d.Idtipotramite)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tipotramite");
    }

}