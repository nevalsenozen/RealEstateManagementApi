using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Dto;
using RealEstateManagement.Data;
using RealEstateManagement.Data.Abstract;
using RealEstateManagement.Entity.Concrete;

namespace RealEstateManagement.Business.Concrete
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PropertyImage> _imageRepository;
        private readonly IRepository<Property> _propertyRepository;
        private readonly IMapper _mapper;

        public PropertyImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _imageRepository = _unitOfWork.GetRepository<PropertyImage>();
            _propertyRepository = _unitOfWork.GetRepository<Property>();
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<PropertyImageDto>>> GetPropertyImagesAsync(int propertyId)
        {
            try
            {
                var property = await _propertyRepository.GetAsync(propertyId);
                if (property is null)
                {
                    return ResponseDto<List<PropertyImageDto>>.Fail("Property not found", StatusCodes.Status404NotFound);
                }

                var images = await _imageRepository.GetAllAsync(x => x.PropertyId == propertyId);
                var dto = _mapper.Map<List<PropertyImageDto>>(images);

                return ResponseDto<List<PropertyImageDto>>.Success(dto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<List<PropertyImageDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<PropertyImageDto>> AddPropertyImageAsync(
            int propertyId,
            PropertyImageCreateDto dto)
        {
            try
            {
                var property = await _propertyRepository.GetAsync(propertyId);
                if (property is null)
                {
                    return ResponseDto<PropertyImageDto>.Fail("Property not found", StatusCodes.Status404NotFound);
                }

                var image = _mapper.Map<PropertyImage>(dto);
                image.PropertyId = propertyId;

                await _imageRepository.AddAsync(image);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<PropertyImageDto>.Fail("Database error occurred while adding image", StatusCodes.Status500InternalServerError);
                }

                var resultDto = _mapper.Map<PropertyImageDto>(image);
                return ResponseDto<PropertyImageDto>.Success(resultDto, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return ResponseDto<PropertyImageDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<PropertyImageDto>> UpdatePropertyImageAsync(
            int propertyId,
            int imageId,
            PropertyImageUpdateDto dto)
        {
            try
            {
                var image = await _imageRepository.GetAsync(x => x.Id == imageId && x.PropertyId == propertyId);

                if (image is null)
                {
                    return ResponseDto<PropertyImageDto>.Fail("Image not found", StatusCodes.Status404NotFound);
                }

                _mapper.Map(dto, image);
                _imageRepository.Update(image);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<PropertyImageDto>.Fail("Database error occurred while updating image", StatusCodes.Status500InternalServerError);
                }

                var resultDto = _mapper.Map<PropertyImageDto>(image);
                return ResponseDto<PropertyImageDto>.Success(resultDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<PropertyImageDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<bool>> DeletePropertyImageAsync(int propertyId, int imageId)
        {
            try
            {
                var image = await _imageRepository.GetAsync(x => x.Id == imageId && x.PropertyId == propertyId);

                if (image is null)
                {
                    return ResponseDto<bool>.Fail("Image not found", StatusCodes.Status404NotFound);
                }

                _imageRepository.Remove(image);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<bool>.Fail("Database error occurred while deleting image", StatusCodes.Status500InternalServerError);
                }

                return ResponseDto<bool>.Success(true, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<bool>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
