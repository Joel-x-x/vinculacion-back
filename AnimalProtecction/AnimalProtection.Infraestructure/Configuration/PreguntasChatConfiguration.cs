using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class PreguntaschatConfiguration:IEntityTypeConfiguration<Preguntaschat>
{
    public void Configure(EntityTypeBuilder<Preguntaschat> entity)
    {
        entity.HasKey(e => e.Id).HasName("preguntaschat_pkey");

        entity.ToTable("preguntaschat");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Pregunta)
            .HasMaxLength(200)
            .HasColumnName("pregunta");
    }

}