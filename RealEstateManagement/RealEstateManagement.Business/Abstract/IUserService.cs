using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Abstract;

public interface IUserService
{
    Task<ResponseDto<AppUserDto>> GetMyProfileAsync(string userId);
    Task<ResponseDto<NoContent>> UpdateMyProfileAsync(string userId, UserUpdateDto userUpdateDto);
    Task<ResponseDto<IEnumerable<AppUserDto>>> GetAllUsersAsync();
    Task<ResponseDto<AppUserDto>> GetUserByIdAsync(string userId);
    Task<ResponseDto<NoContent>> UpdateUserRoleAsync(string userId, UserRoleUpdateDto userRoleUpdateDto);
}
