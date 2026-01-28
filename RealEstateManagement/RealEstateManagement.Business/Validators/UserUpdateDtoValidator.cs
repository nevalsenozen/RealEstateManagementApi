using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators
{
    public class UserUpdateDtoValidator 
        : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .MinimumLength(2)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.FirstName))
                .WithMessage("Ad alanı en az 2, en fazla 50 karakter olmalıdır.");

            RuleFor(x => x.LastName)
                .MinimumLength(2)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.LastName))
                .WithMessage("Soyad alanı en az 2, en fazla 50 karakter olmalıdır.");
        }
    }
}
