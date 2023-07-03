using System.ComponentModel.DataAnnotations;
using FluentValidation;
using LibraryManagement.Core.Exception;
using MediatR;

namespace LibraryManagement.Core.Middleware
{
    // Middleware for request validation using FluentValidation.
    public sealed class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        // Initializes a new instance of the RequestValidationBehavior class.
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        // Handles the request by performing validation and invoking the next behavior in the pipeline.
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext(request);
            Validator.ValidateObject(request, context, true);

            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                throw new BadRequestException(failures);
            }

            return await next();
        }
    }
}
