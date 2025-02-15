using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class PreguntasFrecuentesConfiguration : IEntityTypeConfiguration<PreguntasFrecuentes>
{
    public void Configure(EntityTypeBuilder<PreguntasFrecuentes> entity)
    {
        entity.ToTable("preguntasfrecuentes");

        entity.HasKey(e => e.Id).HasName("preguntasfrecuentes_pkey");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");

        entity.Property(e => e.Pregunta)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("pregunta");

        entity.Property(e => e.Respuesta)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("respuesta");

        entity.Property(e => e.Prioridad)
            .HasColumnName("prioridad");

        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
    }
}