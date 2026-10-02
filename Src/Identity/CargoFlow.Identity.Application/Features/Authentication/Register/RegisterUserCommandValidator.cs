using FluentValidation;

namespace CargoFlow.Identity.Application.Features.Authentication.Register;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
        .NotEmpty()
        .MaximumLength(100);

        RuleFor(x => x.LastName)
        .NotEmpty()
        .MaximumLength(100);

        RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();

        RuleFor(x => x.Password)
        .NotEmpty()
        .MinimumLength(8);
    }
};
