using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Dto;
using RealEstateManagement.Data.Abstract;
using RealEstateManagement.Entity.Concrete;

namespace RealEstateManagement.Business.Concrete
{
    public class InquiryService : IInquiryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Inquiry> _inquiryRepository;
        private readonly IRepository<Property> _propertyRepository;
        private readonly IMapper _mapper;

        public InquiryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _inquiryRepository = _unitOfWork.GetRepository<Inquiry>();
            _propertyRepository = _unitOfWork.GetRepository<Property>();
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<InquiryCreateDto>>> GetAllInquiriesAsync()
        {
            try
            {
                var inquiries = await _inquiryRepository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<InquiryCreateDto>>(inquiries);
                return ResponseDto<IEnumerable<InquiryCreateDto>>.Success(dtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<InquiryCreateDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<InquiryCreateDto>> GetInquiryByIdAsync(int id)
        {
            try
            {
                var inquiry = await _inquiryRepository.GetAsync(id);
                if (inquiry is null)
                {
                    return ResponseDto<InquiryCreateDto>.Fail("Inquiry not found", StatusCodes.Status404NotFound);
                }

                var dto = _mapper.Map<InquiryCreateDto>(inquiry);
                return ResponseDto<InquiryCreateDto>.Success(dto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<InquiryCreateDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<InquiryCreateDto>> CreateInquiryAsync(InquiryCreateDto inquiryCreateDto)
        {
            try
            {
                var property = await _propertyRepository.GetAsync(inquiryCreateDto.PropertyId);
                if (property is null)
                {
                    return ResponseDto<InquiryCreateDto>.Fail("Property not found", StatusCodes.Status404NotFound);
                }

                var inquiry = _mapper.Map<Inquiry>(inquiryCreateDto);
                await _inquiryRepository.AddAsync(inquiry);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<InquiryCreateDto>.Fail("Database error occurred while creating inquiry", StatusCodes.Status500InternalServerError);
                }

                var dto = _mapper.Map<InquiryCreateDto>(inquiry);
                return ResponseDto<InquiryCreateDto>.Success(dto, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return ResponseDto<InquiryCreateDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<NoContent>> UpdateInquiryAsync(int id, InquiryUpdateDto inquiryUpdateDto)
        {
            try
            {
                var inquiry = await _inquiryRepository.GetAsync(id);
                if (inquiry is null)
                {
                    return ResponseDto<NoContent>.Fail("Inquiry not found", StatusCodes.Status404NotFound);
                }

                _mapper.Map(inquiryUpdateDto, inquiry);
                _inquiryRepository.Update(inquiry);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<NoContent>.Fail("Database error occurred while updating inquiry", StatusCodes.Status500InternalServerError);
                }

                return ResponseDto<NoContent>.Success(new NoContent(), StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<NoContent>> DeleteInquiryAsync(int id)
        {
            try
            {
                var inquiry = await _inquiryRepository.GetAsync(id);
                if (inquiry is null)
                {
                    return ResponseDto<NoContent>.Fail("Inquiry not found", StatusCodes.Status404NotFound);
                }

                _inquiryRepository.Remove(inquiry);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<NoContent>.Fail("Database error occurred while deleting inquiry", StatusCodes.Status500InternalServerError);
                }

                return ResponseDto<NoContent>.Success(new NoContent(), StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<IEnumerable<InquiryCreateDto>>> GetMyInquiriesAsync(string userId)
        {
            try
            {
                var inquiries = await _inquiryRepository.GetAllAsync(x => x.UserId == userId);
                var dtos = _mapper.Map<IEnumerable<InquiryCreateDto>>(inquiries);
                return ResponseDto<IEnumerable<InquiryCreateDto>>.Success(dtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<InquiryCreateDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
