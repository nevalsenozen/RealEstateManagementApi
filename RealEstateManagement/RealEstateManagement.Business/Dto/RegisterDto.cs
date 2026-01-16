
using System.ComponentModel.DataAnnotations;

namespace RealEstateManagement.Business.Dto;

public class RegisterDto
{
    public string FirstName { get; set; }=string.Empty;
    public string LastName { get; set; } = string.Empty;

    ///[EmailAddress(ErrorMessage = "Lütfen geçerli bir email adresi giriniz!")]
    //[Required(ErrorMessage = "Email bilgisi zorunludur!")]
    public string Email { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Parola zorunludur!")]
    //[StringLength(20, ErrorMessage = "Parola en fazla 20, en az 8 karakter olabilir!", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;

    //[Compare("Password", ErrorMessage = "Parolalar eşleşmiyor!")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string PhoneNumber {get; set; } = string.Empty;
}
