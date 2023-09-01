namespace Blazor_MediatR2.Components;

public class ProjectException : Exception
{
    public ProjectException(string message) : base(message) { }
}