using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators
{
    public class InquiryUpdateDtoValidator 
        : AbstractValidator<InquiryUpdateDto>
    {
        public InquiryUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Güncellenecek sorgu için geçerli bir Id gönderilmelidir.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Sorgu durumu boş olamaz.")
                .Must(BeValidStatus)
                .WithMessage("Geçersiz sorgu durumu. Geçerli değerler: Yeni Sorgu, İletişime Geçildi, Çözüldü, Kapatıldı.");
        }

        private bool BeValidStatus(string status)
        {
            var validStatuses = new[]
            {
                "Yeni Sorgu",
                "İletişime Geçildi",
                "Çözüldü",
                "Kapatıldı"
            };

            return validStatuses.Contains(status);
        }
    }
}