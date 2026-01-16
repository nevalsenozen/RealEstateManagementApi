
using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad bilgisi zorunludur!")
            .MinimumLength(2).WithMessage("Ad bilgisi en az 2 karakter olmalıdır!")
            .MaximumLength(50).WithMessage("Ad bilgisi en fazla 50 karakter olabilir!")
            .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ:\s]+$").WithMessage("Ad bilgisi sadece harf içerebilir!");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad bilgisi zorunludur!")
            .MinimumLength(2).WithMessage("Soyad bilgisi en az 2 karakter olmalıdır!")
            .MaximumLength(50).WithMessage("Soyad bilgisi en fazla 50 karakter olabilir!")
            .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ:\s]+$").WithMessage("Soyad bilgisi sadece harf içerebilir!");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email bilgisi zorunludur")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
            .MaximumLength(100).WithMessage("Email en fazla 100 karakter olabilir!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre zorunludur!")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmaldır!")
            .MaximumLength(50).WithMessage("Şifre en fazla 50 karakter olabilir!")
            .Matches(@"[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir!")
            .Matches(@"[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir!")
            .Matches(@"[0-9]").WithMessage("Şifre en az bir rakam içermelidir!")
            .Matches(@"[!@#$%^&*(),.?"":{}|<>]").WithMessage("Şifre en az bir özel karakter içermelidir!");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Şifre tekrarı zorunludur!")
            .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor!");

    }
}
