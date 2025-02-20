namespace AnimalProtection.Domain.Entities;
public class DatosInstitucion
{
    public Guid Id { get; set; }
    
    public string Nombre { get; set; }
    
    public string ColorPagina { get; set; }
    
    public string UrlLogo { get; set; }
    
    public string Ubicacion { get; set; }
    
    public string QuienesSomos { get; set; }
    
    public string Mision { get; set; }
    
    public string Vision { get; set; }
    
    public string ColorPrincipal { get; set; }

    public string ColorSecundario { get; set; }

    public Guid? IdArchivoLogo { get; set; }
    
    public bool EstaActivo { get; set; }


    // Virtuals
    public virtual ICollection<RedesSociales> ReedesSociales { get; set; }
}
