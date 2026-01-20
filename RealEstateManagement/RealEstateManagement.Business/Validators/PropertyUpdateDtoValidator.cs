using System;
using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators;

public class PropertyUpdateDtoValidator : AbstractValidator<PropertyUpdateDto>
{
    public PropertyUpdateDtoValidator()
    {
        Include(new PropertyCreateDtoValidator());
        
        RuleFor(x =>x.Id)
        .NotEmpty().WithMessage("Id girmek zorunludur!")
        .GreaterThan(0).WithMessage("Id O'dan büyük olmalıdır!");
    }
}