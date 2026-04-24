using Backend.Modules.Absences.Api.Requests;
using Backend.Modules.Absences.Api.Requests.Create;
using FluentValidation;

namespace Backend.Modules.Absences.Api.Create;

public sealed class CreateAbsenceValidator
    : AbstractValidator<CreateAbsenceRequest>
{
    public CreateAbsenceValidator()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        RuleFor(x => x.StartDate)
            .GreaterThanOrEqualTo(today)
            .WithMessage("Start date cannot be in the past.");

        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("End date must be after or equal to start date.");
    }
}
