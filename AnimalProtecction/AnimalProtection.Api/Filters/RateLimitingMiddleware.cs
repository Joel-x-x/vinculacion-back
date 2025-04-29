using System.Collections.Concurrent;
using AnimalProtection.Domain.Shared;
using Microsoft.Extensions.Options;

namespace ClinicMedicalAppointments.Api.Filters;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _maxRequests;
    private readonly TimeSpan _timeWindow;
    private static readonly ConcurrentDictionary<string, RequestCounter> _requestCounters = new();

    public RateLimitingMiddleware(RequestDelegate next, IOptions<RateLimitingSettings> options)
    {
        _next = next;
        _maxRequests = options.Value.MaxRequests;
        _timeWindow = options.Value.TimeWindow;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var clientIp = context.Connection.RemoteIpAddress?.ToString();

        if (string.IsNullOrEmpty(clientIp))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Client IP not found.");
            return;
        }

        var counter = _requestCounters.GetOrAdd(clientIp, _ => new RequestCounter());

        lock (counter)
        {
            if (counter.RequestCount >= _maxRequests && counter.Timestamp > DateTime.UtcNow - _timeWindow)
            {
                var remoteIp = context.Connection.RemoteIpAddress;
                var remotePort = context.Connection.RemotePort;
                var localIp = context.Connection.LocalIpAddress;
                var localPort = context.Connection.LocalPort;
                
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.Response.Headers["Retry-After"] = _timeWindow.TotalSeconds.ToString();
                context.Response.WriteAsync($"Demasiadas solicitudes, desde la ip loca: {localIp}, con puerto: {localPort}, ip remota: {remoteIp}, puerto {remotePort}.");
                return;
            }

            if (counter.Timestamp <= DateTime.UtcNow - _timeWindow)
            {
                counter.Reset();
            }

            counter.Increment();
        }

        await _next(context);
    }

    private class RequestCounter
    {
        public int RequestCount { get; private set; }
        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

        public void Increment()
        {
            RequestCount++;
            Timestamp = DateTime.UtcNow;
        }

        public void Reset()
        {
            RequestCount = 1;
            Timestamp = DateTime.UtcNow;
        }
    }
}
