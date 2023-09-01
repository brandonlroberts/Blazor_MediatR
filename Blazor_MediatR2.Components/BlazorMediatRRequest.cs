namespace Blazor_MediatR2.Components;

public record BlazorMediatRRequest
{
    public string? Requestor { get; set; }
}

public record AuditableResponse
{
    public string? ResponseType { get; set; }
    public string? AuditMessage { get; set; }
    public string? Requestor { get; set; }
}