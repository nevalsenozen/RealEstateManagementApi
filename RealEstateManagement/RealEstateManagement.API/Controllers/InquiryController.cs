using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.API.Controllers;
using RealEstateManagement.Business.Dto;

namespace RealEstate.API.Controllers
{
    [Route("api/inquiries")]
    [ApiController]
    public class InquiriesController : CustomControllerBase
    {
       
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllInquiries()
        {
            return base.Ok();
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInquiryById(int id)
        {
            return base.Ok();
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateInquiry([FromBody] InquiryCreateDto inquiryCreateDto)
        {
            return base.Ok();
        }

        
        [Authorize(Roles = "Admin,Agent")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInquiry(int id, [FromBody] InquiryUpdateDto inquiryUpdateDto)
        {
            return base.Ok();
        }

       
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInquiry(int id)
        {
            return base.Ok();
        }

        
        [Authorize]
        [HttpGet("my-inquiries")]
        public async Task<IActionResult> GetMyInquiries()
        {
            return base.Ok();
        }
    }
}
