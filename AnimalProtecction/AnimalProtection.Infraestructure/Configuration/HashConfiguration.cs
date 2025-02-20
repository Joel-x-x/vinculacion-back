using AnimalProtection.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalProtecction.Configuration;

public class HashConfiguration:IEntityTypeConfiguration<Hash>
{
    public void Configure(EntityTypeBuilder<Hash> entity)
    {
        entity.HasKey(e => e.Id).HasName("hash_pkey");

        entity.ToTable("hash");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("id");
        entity.Property(e => e.Clave)
            .HasMaxLength(100)
            .HasColumnName("clave");
        entity.Property(e => e.Estaactivo)
            .HasDefaultValue(true)
            .HasColumnName("estaactivo");
        entity.Property(e => e.Fechaactualizacion)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("fechaactualizacion");
        entity.Property(e => e.Idusuario).HasColumnName("idusuario");

        entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Hashes)
            .HasForeignKey(d => d.Idusuario)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_hashusuario");
    }

}