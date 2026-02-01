using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Dto;
using RealEstateManagement.Data.Abstract;
using RealEstateManagement.Entity.Concrete;

namespace RealEstateManagement.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AppUser> _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(
            IUnitOfWork unitOfWork,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.GetRepository<AppUser>();
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ResponseDto<AppUserDto>> GetMyProfileAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return ResponseDto<AppUserDto>.Fail("User not found", StatusCodes.Status404NotFound);
                }

                var dto = _mapper.Map<AppUserDto>(user);
                return ResponseDto<AppUserDto>.Success(dto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<AppUserDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<NoContent>> UpdateMyProfileAsync(string userId, UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return ResponseDto<NoContent>.Fail("User not found", StatusCodes.Status404NotFound);
                }

                user.FirstName = userUpdateDto.FirstName;
                user.LastName = userUpdateDto.LastName;
                //user.PhoneNumber = userUpdateDto.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return ResponseDto<NoContent>.Fail(errors, StatusCodes.Status400BadRequest);
                }

                return ResponseDto<NoContent>.Success(new NoContent(), StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<IEnumerable<AppUserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<AppUserDto>>(users);
                return ResponseDto<IEnumerable<AppUserDto>>.Success(dtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<AppUserDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<AppUserDto>> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return ResponseDto<AppUserDto>.Fail("User not found", StatusCodes.Status404NotFound);
                }

                var dto = _mapper.Map<AppUserDto>(user);
                return ResponseDto<AppUserDto>.Success(dto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<AppUserDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<NoContent>> UpdateUserRoleAsync(string userId, UserRoleUpdateDto userRoleUpdateDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return ResponseDto<NoContent>.Fail("User not found", StatusCodes.Status404NotFound);
                }

                /*var roleExists = await _roleManager.RoleExistsAsync(userRoleUpdateDto.RoleName);
                if (!roleExists)
                {
                    return ResponseDto<NoContent>.Fail($"Role '{userRoleUpdateDto.RoleName}' not found", StatusCodes.Status400BadRequest);
                }*/

                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                
                /*var result = await _userManager.AddToRoleAsync(user, userRoleUpdateDto.RoleName);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return ResponseDto<NoContent>.Fail(errors, StatusCodes.Status400BadRequest);
                }*/

                return ResponseDto<NoContent>.Success(new NoContent(), StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
