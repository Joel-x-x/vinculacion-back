using AnimalProtection.Domain.Entities;
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

    public virtual DbSet<Adopcione> Adopciones { get; set; }

    public virtual DbSet<Archivo> Archivos { get; set; }

    public virtual DbSet<Archivoscampana> Archivoscampanas { get; set; }

    public virtual DbSet<Archivosmascotum> Archivosmascota { get; set; }

    public virtual DbSet<Archivostramite> Archivostramites { get; set; }

    public virtual DbSet<Campana> Campanas { get; set; }

    public virtual DbSet<Cooperantes> Cooperantes { get; set; }

    public virtual DbSet<DatosInstitucion> Datosinstitucions { get; set; }

    public virtual DbSet<Datosrecuperacion> Datosrecuperacions { get; set; }

    public virtual DbSet<Especy> Especies { get; set; }

    public virtual DbSet<Estadostramite> Estadostramites { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Hash> Hashes { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Mascota> Mascotas { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Menusrol> Menusrols { get; set; }

    public virtual DbSet<Preguntaschat> Preguntaschats { get; set; }

    public virtual DbSet<Preguntasfrecuente> Preguntasfrecuentes { get; set; }

    public virtual DbSet<Raza> Razas { get; set; }

    public virtual DbSet<RedesSociales> Redessociales { get; set; }

    public virtual DbSet<Respuestaschat> Respuestaschats { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Tiposarchivo> Tiposarchivos { get; set; }

    public virtual DbSet<Tiposcampana> Tiposcampanas { get; set; }

    public virtual DbSet<Tiposlog> Tiposlogs { get; set; }

    public virtual DbSet<Tipostramite> Tipostramites { get; set; }

    public virtual DbSet<Tramite> Tramites { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AnimalprotectionContext).Assembly);
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
