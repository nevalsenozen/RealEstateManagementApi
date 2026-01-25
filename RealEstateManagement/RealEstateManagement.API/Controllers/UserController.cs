using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Controllers;
using RealEstateManagement.Business.Dto;

namespace RealEstate.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : CustomControllerBase
    {
        // GET /api/users/me   - Kendi profil bilgilerini getir
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            return base.Ok();
        }

        // PUT /api/users/me   - Kendi profilini güncelle
        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UserUpdateDto userUpdateDto)
        {
            return base.Ok();
        }

        // GET /api/users   - Tüm kullanıcıları listele (Admin)
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return base.Ok();
        }

        // GET /api/users/{id}   - Kullanıcı detayını getir (Admin)
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return base.Ok();
        }

        // PUT /api/users/{id}/role   - Kullanıcı rolünü güncelle (Admin)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateUserRole(
            int id,
            [FromBody] UserRoleUpdateDto userRoleUpdateDto)
        {
            return base.Ok();
        }
    }
}
