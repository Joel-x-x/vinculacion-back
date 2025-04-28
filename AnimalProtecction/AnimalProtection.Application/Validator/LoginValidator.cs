using FluentValidation;

namespace AnimalProtection.Application.Validator;

// public class LoginValidator: AbstractValidator<AuthenticationCommand>
// {
//     public   LoginValidator()
//     {
//         RuleFor(c => c.Identificacion)
//             .EmailAddress()
//             .WithMessage("Invalid identification");
//
//         RuleFor(c => c.Clave)
//             .NotEmpty()
//             .WithMessage("Invalid password");
//     }
// }