namespace AnimalProtecction.Domain.Entities;

public class PreguntasChat
{
    public Guid Id { get; set; }
    
    public string Pregunta { get; set; }
    
    public bool EstaActivo { get; set; }
    
    // Virtuals
    public ICollection<RespuestasChat> RespuestasChats { get; set; }
}