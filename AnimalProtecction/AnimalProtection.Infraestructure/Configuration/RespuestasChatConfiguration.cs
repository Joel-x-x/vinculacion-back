using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class RespuestasChatConfiguration : IEntityTypeConfiguration<RespuestasChat>
{
    public void Configure(EntityTypeBuilder<RespuestasChat> entity)
    {
        entity.ToTable("respuestaschat");

        entity.HasKey(e => e.Id).HasName("respuestaschat_pkey");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");

        entity.Property(e => e.Respuesta)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("respuesta");

        entity.Property(e => e.IdPregunta)
            .IsRequired()
            .HasColumnName("idpregunta");

        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");

        // Relación con PreguntasChat
        entity.HasOne(e => e.PreguntasChat)
            .WithMany() // Si `PreguntasChat` tiene `public List<RespuestasChat> Respuestas`, aquí pon `.WithMany(p => p.Respuestas)`
            .HasForeignKey(e => e.IdPregunta)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_preguntaschat");
    }
}