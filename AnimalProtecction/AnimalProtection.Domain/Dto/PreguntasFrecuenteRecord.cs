using AnimalProtection.Domain.Entities;

namespace AnimalProtection.Domain.Dto;

public record PreguntasFrecuenteRecord(
    Guid Id,
    string Pregunta,
    string Respuesta,
    int Prioridad,
    bool Estaactivo
)
{
    public PreguntasFrecuenteRecord(Preguntasfrecuente preguntasfrecuente) : this(
        preguntasfrecuente.Id,
        preguntasfrecuente.Pregunta,
        preguntasfrecuente.Respuesta,
        preguntasfrecuente.Prioridad,
        preguntasfrecuente.Estaactivo ?? false
    )
    { }
}

public record PreguntasFrecuenteCreateRecord(
    Guid Id,
    string Pregunta,
    string Respuesta,
    int Prioridad,
    bool Estaactivo
);

public record PreguntasFrecuenteUpdateRecord(
    Guid Id,
    string Pregunta,
    string Respuesta,
    int Prioridad,
    bool Estaactivo
);


