using Backend.Modules.Absences.Api.Requests;
using FluentValidation;

namespace Backend.Modules.Absences.Api;

public sealed class CreateAbsenceValidator
    : AbstractValidator<CreateAbsenceRequest>
{
    public CreateAbsenceValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        RuleFor(x => x.StartDate)
            .GreaterThanOrEqualTo(today)
            .WithMessage("Start date cannot be in the past.");

        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("End date must be after or equal to start date.");
    }
}
