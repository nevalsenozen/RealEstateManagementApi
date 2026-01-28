using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Abstract
{
    public interface IPropertyImageService
    {
        Task<ResponseDto<List<PropertyImageDto>>> GetPropertyImagesAsync(int propertyId);

        Task<ResponseDto<PropertyImageDto>> AddPropertyImageAsync(
            int propertyId,
            PropertyImageCreateDto dto);

        Task<ResponseDto<PropertyImageDto>> UpdatePropertyImageAsync(
            int propertyId,
            int imageId,
            PropertyImageUpdateDto dto);

        Task<ResponseDto<bool>> DeletePropertyImageAsync(int propertyId, int imageId);
    }
}