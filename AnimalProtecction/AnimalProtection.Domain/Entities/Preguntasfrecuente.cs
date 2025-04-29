using AnimalProtection.Domain.Dto;

namespace AnimalProtection.Domain.Entities;

public partial class Preguntasfrecuente
{
    public Guid Id { get; set; }

    public string Pregunta { get; set; } = null!;

    public string Respuesta { get; set; } = null!;

    public int Prioridad { get; set; }

    public bool? Estaactivo { get; set; }



    // Crear el método para generar uan pregunta frecuente
    public static Preguntasfrecuente CreateFromRecord(PreguntasFrecuenteCreateRecord preguntasFrecuenteCreateRecord)
    {
        return new Preguntasfrecuente
        {
            Id = Guid.NewGuid(),
            Pregunta = preguntasFrecuenteCreateRecord.Pregunta,
            Respuesta = preguntasFrecuenteCreateRecord.Respuesta,
            Prioridad = preguntasFrecuenteCreateRecord.Prioridad,
            Estaactivo = true
        };
    }
    // Actualizar una pregunta Frecuente
    public void UpdateFromRecord(PreguntasFrecuenteUpdateRecord preguntasFrecuenteUpdateRecord)
    {
        if (!string.Equals(Pregunta, preguntasFrecuenteUpdateRecord.Pregunta, StringComparison.Ordinal))
            Pregunta = preguntasFrecuenteUpdateRecord.Pregunta;


        if (!string.Equals(Respuesta, preguntasFrecuenteUpdateRecord.Respuesta, StringComparison.Ordinal))
            Respuesta = preguntasFrecuenteUpdateRecord.Respuesta;


        if (Prioridad != preguntasFrecuenteUpdateRecord.Prioridad)
            Prioridad = preguntasFrecuenteUpdateRecord.Prioridad;
    }

    public void eliminar()
    {
        Estaactivo = false;
    }


}
