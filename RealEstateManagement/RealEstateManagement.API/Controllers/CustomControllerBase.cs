using RealEstateManagement.Business.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateManagement.API.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected static IActionResult CreateResult<T>(ResponseDto<T> responseDto)
        {
            return new ObjectResult(responseDto) { StatusCode = responseDto.StatusCode };
        }
    }
}
