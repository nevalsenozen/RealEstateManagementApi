using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Abstract;

public interface IInquiryService
{
    Task<ResponseDto<IEnumerable<InquiryCreateDto>>> GetAllInquiriesAsync();
    Task<ResponseDto<InquiryCreateDto>> GetInquiryByIdAsync(int id);
    Task<ResponseDto<InquiryCreateDto>> CreateInquiryAsync(InquiryCreateDto inquiryCreateDto);
    Task<ResponseDto<NoContent>> UpdateInquiryAsync(int id, InquiryUpdateDto inquiryUpdateDto);
    Task<ResponseDto<NoContent>> DeleteInquiryAsync(int id);
    Task<ResponseDto<IEnumerable<InquiryCreateDto>>> GetMyInquiriesAsync(string userId);
}
