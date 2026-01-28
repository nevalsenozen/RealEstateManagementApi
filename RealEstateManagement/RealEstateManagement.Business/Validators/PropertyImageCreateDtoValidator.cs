using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators
{
    public class PropertyImageCreateDtoValidator 
        : AbstractValidator<PropertyImageCreateDto>
    {
        public PropertyImageCreateDtoValidator()
        {
            // Resim URL
            RuleFor(x => x.ImageUrl)
                .NotEmpty()
                .WithMessage("Resim adresi boş olamaz.")
                .Must(BeValidUrl)
                .WithMessage("Geçerli bir resim adresi giriniz.");

            // İlan (Property) Id
            RuleFor(x => x.PropertyId)
                .GreaterThan(0)
                .WithMessage("Geçerli bir ilan bilgisi gönderilmelidir.");

            // Görüntülenme sırası
            RuleFor(x => x.DisplayOrder)
                .GreaterThan(0)
                .WithMessage("Görüntülenme sırası 1 veya daha büyük olmalıdır.");

            // Ana resim bilgisi
            RuleFor(x => x.IsPrimary)
                .NotNull()
                .WithMessage("Ana resim bilgisi belirtilmelidir.");
        }

        private bool BeValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
