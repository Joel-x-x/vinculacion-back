namespace AnimalProtection.Domain.Entities;

public class RedesSociales
{
    public Guid Id { get; set; }
    
    public string Nombre { get; set; }
    
    public string UrlRedSocial { get; set; }
    
    public Guid? IdArchivoLogo { get; set; }

    public bool EstaActivo { get; set; }

    public Guid IdInstitucion { get; set; }


    // Virtuals
    public virtual DatosInstitucion DatosInstitucion { get; set; }
}