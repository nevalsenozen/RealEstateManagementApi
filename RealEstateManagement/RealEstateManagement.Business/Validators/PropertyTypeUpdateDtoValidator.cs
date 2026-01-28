using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators
{
    public class PropertyTypeUpdateDtoValidator 
        : AbstractValidator<PropertyTypeUpdateDto>
    {
        public PropertyTypeUpdateDtoValidator()
        {
            
            Include(new PropertyTypeCreateDtoValidator());

            
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Güncellencek emlak tipi için geçerli bir Id gönderilmelidir.");
        }
    }
}
