using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Validation
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting validation for {RequestType}", typeof(TRequest).Name);

            // Perform validation
            var context = new ValidationContext<TRequest>(request);
            var validationResults = _validators.Select(v => v.Validate(context)).ToList();

            if (validationResults.Any(r => !r.IsValid))
            {
                var errors = validationResults.SelectMany(r => r.Errors).Select(e => e.ErrorMessage).ToList();
                var errorMessage = string.Join(", ", errors);

                _logger.LogWarning("Validation failed for {RequestType}: {Errors}", typeof(TRequest).Name, errorMessage);

                var failureResponse = typeof(TResponse).GetMethod("Failure")?.Invoke(null, new object[] { errorMessage, "Validation failed" });
                return (TResponse)failureResponse!;
            }

            _logger.LogInformation("Validation succeeded for {RequestType}", typeof(TRequest).Name);

            var response = await next();

            return response;
        }
    }
}
