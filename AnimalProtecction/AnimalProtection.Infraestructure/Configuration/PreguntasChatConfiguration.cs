using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class PreguntasChatConfiguration : IEntityTypeConfiguration<PreguntasChat>
{
    public void Configure(EntityTypeBuilder<PreguntasChat> entity)
    {
        entity.ToTable("preguntaschat");

        entity.HasKey(e => e.Id).HasName("preguntaschat_pkey");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");

        entity.Property(e => e.Pregunta)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("pregunta");

        entity.Property(e => e.EstaActivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
    }
}