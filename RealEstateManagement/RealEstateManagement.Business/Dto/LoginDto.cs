using System.ComponentModel.DataAnnotations;

namespace RealEstateManagement.Business.Dto;

public class LoginDto
{
    //[Required(ErrorMessage = "Email bilgisi zorunludur!")]
    //[EmailAddress(ErrorMessage = "Lütfen geçerli bir email adresi giriniz!")]
    public string Email { get; set; }=string.Empty;

    //[Required(ErrorMessage = "Parola zorunludur!")]
    public string Password { get; set; }=string.Empty;

}
