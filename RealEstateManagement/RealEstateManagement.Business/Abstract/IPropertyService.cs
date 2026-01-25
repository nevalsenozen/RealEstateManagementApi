using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Abstract;

public interface IPropertyService
{
    Task<ResponseDto<PagedResultDto<PropertyDto>>> GetAllPagedAsync(
        PaginationQueryDto paginationQueryDto,
        Expression<Func<Property, bool>>? predicate = null,
        Func<IQueryable<Property>, IOrderedQueryable<Property>>? orderBy = null,
        bool? includeCategories = false,
        int? categoryId = null,
        bool? isDeleted = null);

        Task<ResponseDto<PropertyDto>> GetAsync(int id);

        Task<ResponseDto<IEnumerable<PropertyDto>>> GetAllAsync(
        Expression<Func<Property, bool>>? predicate,
        Func<IQueryable<Property>, IOrderedQueryable<Property>>? orderBy,
        bool? includeCategories = null,
        int? categoryId = null,
        bool? isDeleted = null);


        Task<ResponseDto<PropertyDto>> CreateAsync(PropertyCreateDto propertyCreateDto);
        Task<ResponseDto<PropertyDto>> UpdateAsync(int id, PropertyUpdateDto propertyUpdateDto);
        Task<ResponseDto<PropertyDto>> SoftDeleteAsync(int id);

        Task<ResponseDto<List<PropertyDto>>> GetMyPropertiesAsync(PropertyFilterDto filterDto);

}