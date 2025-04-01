namespace Esms.API.Validators
{
    using Esms.API.Request;
    using FluentValidation;

    public class EmployeeSeriesRequestValidator : AbstractValidator<EmployeeSeriesRequest>
    {
        public EmployeeSeriesRequestValidator()
        {
            RuleFor(x => x.ExternalId)
                .GreaterThan(0).WithMessage("ExternalId must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must be at most 100 characters");

            RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("StartDate must be before or equal to EndDate");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("EndDate must be after or equal to StartDate");
        }
    }
}
