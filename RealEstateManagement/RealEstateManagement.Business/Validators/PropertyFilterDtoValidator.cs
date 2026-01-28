using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators
{
    public class PropertyFilterDtoValidator : AbstractValidator<PropertyFilterDto>
    {
        public PropertyFilterDtoValidator()
        {
            
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Sayfa numarası 1'den büyük olmalıdır.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Sayfa başına kayıtsayısı 1 ile 100 arasında olmalıdır.");

            
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinPrice.HasValue)
                .WithMessage("Minimum fiyat 0'dan küçük olamaz.");

            RuleFor(x => x.MaxPrice)
                .GreaterThan(0)
                .When(x => x.MaxPrice.HasValue)
                .WithMessage("Maksimum fiyat 0'dan büyükolmalıdır.");

            RuleFor(x => x)
                .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice)
                .WithMessage("Minimum fiyat, maksimum fiyattan büyü olamaz.");

           
            RuleFor(x => x.MinRooms)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinRooms.HasValue)
                .WithMessage("Minimum oda sayısı 0'dan küçük olamaz.");

            RuleFor(x => x.MaxRooms)
                .GreaterThan(0)
                .When(x => x.MaxRooms.HasValue)
                .WithMessage("Maksimum oda sayısı 0'dan büyük olmalıdır.");

            RuleFor(x => x)
                .Must(x => !x.MinRooms.HasValue || !x.MaxRooms.HasValue || x.MinRooms <= x.MaxRooms)
                .WithMessage("Minimum oda sayısı, maksimum oda sayısından büyük olamaz.");

            
            RuleFor(x => x.MinArea)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinArea.HasValue)
                .WithMessage("Minimum alan 0'dan küçük olamaz.");

            RuleFor(x => x.MaxArea)
                .GreaterThan(0)
                .When(x => x.MaxArea.HasValue)
                .WithMessage("Maksimum alan 0'dan büyük olmalıdır.");

            RuleFor(x => x)
                .Must(x => !x.MinArea.HasValue || !x.MaxArea.HasValue || x.MinArea <= x.MaxArea)
                .WithMessage("Minimum alan, maksimum alandan büyük olamaz.");

           
            RuleFor(x => x.MinYear)
                .InclusiveBetween(1900, 2100)
                .When(x => x.MinYear.HasValue)
                .WithMessage("Minimum yapım yılı 1900 ile 2100 arasında olmalıdır.");

            RuleFor(x => x.MaxYear)
                .InclusiveBetween(1900, 2100)
                .When(x => x.MaxYear.HasValue)
                .WithMessage("Maksimum yapım yılı 1900 ile 2100 arasında olmalıdır.");

            RuleFor(x => x)
                .Must(x => !x.MinYear.HasValue || !x.MaxYear.HasValue || x.MinYear <= x.MaxYear)
                .WithMessage("Minimum yapım yılı, maksimum yapım yılından büyük olamaz.");

            
            RuleFor(x => x.SortBy)
                .Must(BeValidSortField)
                .WithMessage("Sıralama alanı geçerli değil. Geçerli alanlar: price, area, rooms, createdAt.");

            
            RuleFor(x => x.SortOrder)
                .Must(x => x == "asc" || x == "desc")
                .WithMessage("Sıralama yönü sadece 'asc' veya 'desc' olabilir.");
        }

        private bool BeValidSortField(string sortBy)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return true;

            var validFields = new[] { "price", "area", "rooms", "createdAt" };
            return validFields.Contains(sortBy);
        }
    }
}
