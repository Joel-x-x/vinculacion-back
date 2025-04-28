namespace AnimalProtection.Domain.Shared;

public class RateLimitingSettings
{
    public int MaxRequests { get; set; }
    public TimeSpan TimeWindow { get; set; }
}