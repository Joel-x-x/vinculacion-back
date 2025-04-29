using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class PreguntasfrecuenteConfiguration:IEntityTypeConfiguration<Preguntasfrecuente>
{
    public void Configure(EntityTypeBuilder<Preguntasfrecuente> entity)
    {
        entity.HasKey(e => e.Id).HasName("preguntasfrecuentes_pkey");

        entity.ToTable("preguntasfrecuentes");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Pregunta)
            .HasMaxLength(200)
            .HasColumnName("pregunta");
        entity.Property(e => e.Prioridad).HasColumnName("prioridad");
        entity.Property(e => e.Respuesta)
            .HasMaxLength(200)
            .HasColumnName("respuesta");
    }
}