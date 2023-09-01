using System.Text;
using FluentValidation;
using MediatR;

namespace Blazor_MediatR2.Components
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (!errors.Any()) return next();

            var errorBuilder = new StringBuilder();
            //errorBuilder.AppendLine("Invalid Operation: ");

            foreach (var error in errors)
            {
                errorBuilder.AppendLine(error.ErrorMessage);
            }

            throw new InvalidRequestException(errorBuilder.ToString(), null);
        }
    }

    public class InvalidRequestException : ProjectException
    {
        public string Details { get; }
        public InvalidRequestException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}
