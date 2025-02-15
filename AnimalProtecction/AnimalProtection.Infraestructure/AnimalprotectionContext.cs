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

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RoleUsuario> RoleUsuarios { get; set; }

    public virtual DbSet<Rolmenu> Rolmenus { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    
    public virtual DbSet<DatosInstitucion> DatosInstitucion { get; set; }
    
    public virtual DbSet<RedesSociales> RedesSociales { get; set; }
    
    public virtual DbSet<PreguntasFrecuentes> PreguntasFrecuentes { get; set; }
    
    public virtual DbSet<Cooperantes> Cooperantes { get; set; }
    
    public virtual DbSet<PreguntasChat> PreguntasChats { get; set; }
    
    public virtual DbSet<RespuestasChat> RespuestasChats { get; set; }
    
    public virtual DbSet<TiposCampana> TiposCampana { get; set; }
    
    public virtual DbSet<Campanas> Campanas { get; set; }
    
    public virtual DbSet<ArchivosCampana> ArchivosCampana { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnimalprotectionContext).Assembly);
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
