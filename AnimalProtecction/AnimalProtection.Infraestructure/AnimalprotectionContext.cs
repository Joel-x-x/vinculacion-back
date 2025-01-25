using AnimalProtecction.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalProtecction.Generated;

public partial class AnimalprotectionContext : DbContext
{
    public AnimalprotectionContext()
    {
    }

    public AnimalprotectionContext(DbContextOptions<AnimalprotectionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Rol> Rols { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnimalprotectionContext).Assembly);
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
