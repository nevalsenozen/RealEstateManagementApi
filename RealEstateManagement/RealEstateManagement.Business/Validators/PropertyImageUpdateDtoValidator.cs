using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators
{
    public class PropertyImageUpdateDtoValidator 
        : AbstractValidator<PropertyImageUpdateDto>
    {
        public PropertyImageUpdateDtoValidator()
        {
            
            Include(new PropertyImageCreateDtoValidator());

            
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Güncellenecek resim için geçerli bir Id gönderilmelidir.");
        }
    }
}