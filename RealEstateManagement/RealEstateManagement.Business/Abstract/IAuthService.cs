using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Abstract;

public interface IAuthService
{
    Task<ResponseDto<TokenDto>> LoginAsync(LoginDto loginDto);
    Task<ResponseDto<AppUserDto>> RegisterAsync(RegisterDto registerDto);
}
