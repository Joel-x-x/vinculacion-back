namespace AnimalProtecction.Domain.Entities;

public class Cooperantes
{
    public Guid Id { get; set; }
    
    public string UrlLogo { get; set; }
    
    public string Nombre { get; set; }
    
    public string Descricpion { get; set; }
    
    public string ColorSecundario { get; set; }
    
    public Guid? IdArchivoLogo { get; set; }

    public bool EstaActivo { get; set; }
}