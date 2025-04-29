using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class RespuestaschatConfiguration: IEntityTypeConfiguration<Respuestaschat>
{
    public void Configure(EntityTypeBuilder<Respuestaschat> entity)
    {
        entity.HasKey(e => e.Id).HasName("respuestaschat_pkey");

        entity.ToTable("respuestaschat");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Idpregunta).HasColumnName("idpregunta");
        entity.Property(e => e.Respuesta)
            .HasMaxLength(200)
            .HasColumnName("respuesta");

        entity.HasOne(d => d.IdpreguntaNavigation).WithMany(p => p.Respuestaschats)
            .HasForeignKey(d => d.Idpregunta)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_preguntaschat");
    }
}