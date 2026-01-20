using System;
using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Validators;

public class PropertyCreateDtoValidator : AbstractValidator<PropertyCreateDto>
{
    public PropertyCreateDtoValidator()

    //PropertyTypeId (Emlak Tipi):** Zorunlu, veritabanında mevcut olmalı (yapılmadıııııı)
    {
    RuleFor(x => x.Title)
    .NotEmpty().WithMessage("Başık girmek zorunludur!")
    .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalıdır.")
    .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olmalıdır.");

    RuleFor(x => x.Description)
    .NotEmpty().WithMessage("Açıklama girmek zorunludur!")
    .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalıdır.")
    .MaximumLength(5000).WithMessage("Açıklama en fazla 5000 karakter olmalıdır.");

    RuleFor (x => x.Price)
    .NotEmpty().WithMessage("Fiyat girmek zorunludur!")
    .GreaterThan(999999999).WithMessage("Fiyat en fazla 999.999.999 olmalıdıır.")
    .LessThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");

    RuleFor(x => x.Address)
    .NotEmpty().WithMessage("Adres girmek zorunludur!")
    .MinimumLength(10).WithMessage("Adres en az 10 karakter olmalıdır.")
    .MaximumLength(500).WithMessage("Adres en fazla 500 karakter olmalıdır.");

     RuleFor(x => x.City)
    .NotEmpty().WithMessage("Şehir girmek zorunludur!")
    .MinimumLength(2).WithMessage("Şehir en az 2 karakter olmalıdır.")
    .MaximumLength(100).WithMessage("Şehir en fazla 100 karakter olmalıdır.");

    RuleFor(x => x.District)
    .MaximumLength(100).WithMessage("Şehir en fazla 100 karakter olmalıdır.");

    RuleFor(x => x.Rooms)
    .NotEmpty().WithMessage("Oda sayısı girmek zorunludur!")
    .LessThan(0).WithMessage("Oda sayısı 0'dan büyük olmalıdır.")
    .GreaterThan(20).WithMessage("Oda sayısı 20'den az olmalıdıır.");
    
    RuleFor(x => x.Bathrooms)
    .NotEmpty().WithMessage("Banyo sayısı girmek zorunludur!")
    .LessThan(0).WithMessage("Banyo sayısı 0'dan büyük olmalıdır.")
    .GreaterThan(10).WithMessage("Banyo sayısı 10'dan az olmalıdıır.");

    RuleFor(x => x.Area)
    .NotEmpty().WithMessage("Alan girmek zorunludur!")
    .LessThan(0).WithMessage("Alan 0m²'den büyük olmalıdır.")
    .GreaterThan(100000).WithMessage("Alan 100.000 m²'den küçük olmalıdır.");

    RuleFor(x => x.Floor)
    .NotEmpty().WithMessage("Kat girmek zorunludur!")
    .LessThan(-10).WithMessage("Kat -10'dan büyük olmalıdır.")
    .GreaterThan(100).WithMessage("Kat 100'den küçük olmalıdır.");

    RuleFor(x => x.TotalFloors)
    .NotEmpty().WithMessage("Toplam kat sayısı girmek zorunludur!")
    .LessThan(0).WithMessage("Toplam kat sayısı 0'dan büyük olmalıdır.")
    .GreaterThan(100).WithMessage("Toplam kat sayısı  100'den küçük olmalıdır.");

    RuleFor(x => x.YearBuilt)
    .NotEmpty().WithMessage("Bina yaşını girmek zoruludur.")
    .InclusiveBetween(1900,2100).WithMessage("Bina yaşı 1900 - 2026 yılları arasında olmalıdır.");

    RuleFor(x => x.PropertyTypeId)
    .GreaterThan(0).WithMessage("Emlak tipi seçmek zorunludur!");


    }
}
