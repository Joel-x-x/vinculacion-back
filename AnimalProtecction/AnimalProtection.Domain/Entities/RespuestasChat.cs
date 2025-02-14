namespace AnimalProtecction.Domain.Entities;

public class RespuestasChat
{
    public Guid Id { get; set; }
    
    public string Respuesta { get; set; }
    
    public bool EstaActivo { get; set; }
    
    public Guid IdPregunta { get; set; }
    
    // Virtuals
    public virtual PreguntasChat PreguntasChat { get; set; }
}