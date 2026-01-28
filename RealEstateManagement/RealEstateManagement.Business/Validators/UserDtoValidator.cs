using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Kullanıcı Id alanı boş olamaz.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress()
                .WithMessage("Geçerli bir e-posta adresi girilmelidir.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Ad alanı boş olamaz.")
                .MaximumLength(50)
                .WithMessage("Ad en fazla 50 karakter olabilir.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Soyad alanı boş olamaz.")
                .MaximumLength(50)
                .WithMessage("Soyad en fazla 50 karakter olabilir.");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20)
                .WithMessage("Telefon numarası en fazla 20 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

            RuleFor(x => x.ProfilePicture)
                .MaximumLength(500)
                .WithMessage("Profil fotoğrafı adresi en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.ProfilePicture));

            RuleFor(x => x.AgencyName)
                .NotEmpty()
                .WithMessage("Emlakçı kullanıcılar için ajans adı zorunludur.")
                .When(x => x.IsAgent);

            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .WithMessage("Emlakçı kullanıcılar için lisans numarası zorunludur.")
                .When(x => x.IsAgent);

            RuleFor(x => x.Roles)
                .NotEmpty()
                .WithMessage("Kullanıcının en az bir rolü olmalıdır.");

            RuleFor(x => x.CreatedAt)
                .NotEqual(default(DateTime))
                .WithMessage("Oluşturulma tarihi geçerli olmalıdır.");
        }
    }
}