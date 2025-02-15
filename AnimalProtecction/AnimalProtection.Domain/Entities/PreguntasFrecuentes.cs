namespace AnimalProtecction.Domain.Entities;

public class PreguntasFrecuentes
{
    public Guid Id { get; set; }
    
    public string Pregunta { get; set; }
    
    public string Respuesta { get; set; }
    
    public int Prioridad { get; set; }
    
    public bool EstaActivo { get; set; }
}