using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Abstract;

public interface IPropertyTypeService
{
    Task<ResponseDto<PropertyTypeCreateDto>> CreateAsync(PropertyTypeCreateDto propertyTypeCreateDto);
    Task<ResponseDto<PropertyTypeCreateDto>> GetAsync(int id);
    Task<ResponseDto<IEnumerable<PropertyTypeCreateDto>>> GetAllAsync();
    Task<ResponseDto<PagedResultDto<PropertyTypeCreateDto>>> GetAllPagedAsync(PaginationQueryDto paginationQueryDto);
    Task<ResponseDto<NoContent>> UpdateAsync(int id, PropertyTypeUpdateDto propertyTypeUpdateDto);
    Task<ResponseDto<NoContent>> DeleteAsync(int id);
}
