using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Controllers;
using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Dto;
using System.Security.Claims;

namespace RealEstate.API.Controllers
{
    [Route("api/inquiries")]
    [ApiController]
    public class InquiriesController : CustomControllerBase
    {
        private readonly IInquiryService _inquiryService;

        public InquiriesController(IInquiryService inquiryService)
        {
            _inquiryService = inquiryService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllInquiries()
        {
            var result = await _inquiryService.GetAllInquiriesAsync();
            return CreateResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInquiryById(int id)
        {
            var result = await _inquiryService.GetInquiryByIdAsync(id);
            return CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInquiry([FromBody] InquiryCreateDto inquiryCreateDto)
        {
            var result = await _inquiryService.CreateInquiryAsync(inquiryCreateDto);
            return CreateResult(result);
        }

        [Authorize(Roles = "Admin,Agent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInquiry(int id, [FromBody] InquiryUpdateDto inquiryUpdateDto)
        {
            var result = await _inquiryService.UpdateInquiryAsync(id, inquiryUpdateDto);
            return CreateResult(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInquiry(int id)
        {
            var result = await _inquiryService.DeleteInquiryAsync(id);
            return CreateResult(result);
        }

        [Authorize]
        [HttpGet("my-inquiries")]
        public async Task<IActionResult> GetMyInquiries()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await _inquiryService.GetMyInquiriesAsync(userId);
            return CreateResult(result);
        }
    }
}
