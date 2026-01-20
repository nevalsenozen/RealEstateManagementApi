using System;
using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators;

public class InquiryCreateDtoValidator : AbstractValidator<InquiryCreateDto>
{
    public InquiryCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("İsim zorunludur.")
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email zorunludur.")
            .EmailAddress().WithMessage("Geçerli bir email giriniz.");

        RuleFor(x => x.Phone)
            .MaximumLength(15);

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Mesaj zorunludur.")
            .MinimumLength(10)
            .MaximumLength(1000);

        RuleFor(x => x.PropertyId)
            .GreaterThan(0).WithMessage("Geçerli bir ilan seçiniz.");
    }
}
