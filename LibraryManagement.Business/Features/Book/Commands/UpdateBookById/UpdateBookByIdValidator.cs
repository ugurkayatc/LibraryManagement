using FluentValidation;

namespace LibraryManagement.Business.Features.Book.Commands.UpdateBookById
{
    // Represents the validator for the UpdateBookByIdRequest class.
    public class UpdateBookByIdValidator : AbstractValidator<UpdateBookByIdRequest>
    {
        // Initializes a new instance of the UpdateBookByIdValidator class.
        public UpdateBookByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.AuthorName).NotEmpty().WithMessage("AuthorName is required");
            RuleFor(x => x.PublisherName).NotEmpty().WithMessage("PublisherName is required");
            RuleFor(x => x.PageCount).NotEmpty().WithMessage("PageCount is required").GreaterThan(0).WithMessage("PageCount must be greater than 0");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.PublicationDate).NotEmpty().WithMessage("PublicationDate is required");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stock is required").GreaterThan(0).WithMessage("Stock must be greater than 0");
            RuleFor(x => x.LoanedCount).NotEmpty().WithMessage("LoanedCount is required");
        }
    }
}
