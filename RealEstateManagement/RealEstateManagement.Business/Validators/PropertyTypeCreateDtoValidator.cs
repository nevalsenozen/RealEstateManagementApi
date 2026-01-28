using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators
{
    public class PropertyTypeCreateDtoValidator 
        : AbstractValidator<PropertyTypeCreateDto>
    {
        public PropertyTypeCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Emlak tipi adı boş olaaz.")
                .MinimumLength(2)
                .WithMessage("Emlak tipi adı en az 2 karakter olmalıdır.")
                .MaximumLength(50)
                .WithMessage("Emlak tipi adı n fazla 50 karakter olabilir.");

            RuleFor(x => x.Description)
                .MaximumLength(250)
                .WithMessage("Açıklama en fazla 250 karakter olabilir.");
        }
    }
}
