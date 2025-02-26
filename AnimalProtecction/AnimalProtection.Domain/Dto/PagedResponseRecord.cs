namespace AnimalProtection.Domain.Dto;

public record PagedResponseRecord<T>(
    List<T> Items, 
    int PageNumber,
    int PageSize,
    int TotalCount, 
    int TotalPages
);