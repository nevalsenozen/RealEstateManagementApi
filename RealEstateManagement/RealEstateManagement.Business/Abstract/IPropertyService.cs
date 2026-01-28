using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Abstract;

public interface IPropertyService
{
    Task<ResponseDto<PagedResultDto<PropertyCreateDto>>> GetAllPagedAsync(
        PaginationQueryDto paginationQueryDto,
        Expression<Func<Property, bool>>? predicate = null,
        Func<IQueryable<Property>, IOrderedQueryable<Property>>? orderBy = null,
        bool? includeCategories = false,
        int? categoryId = null,
        bool? isDeleted = null);

        Task<ResponseDto<PropertyCreateDto>> GetAsync(int id);

        Task<ResponseDto<IEnumerable<PropertyCreateDto>>> GetAllAsync(
        Expression<Func<Property, bool>>? predicate,
        Func<IQueryable<Property>, IOrderedQueryable<Property>>? orderBy,
        bool? includeCategories = null,
        int? categoryId = null,
        bool? isDeleted = null);


        Task<ResponseDto<PropertyCreateDto>> CreateAsync(PropertyCreateDto propertyCreateDto);
        Task<ResponseDto<NoContent>> UpdateAsync(int id, PropertyUpdateDto propertyUpdateDto);
        Task<ResponseDto<NoContent>> SoftDeleteAsync(int id);

        Task<ResponseDto<List<PropertyCreateDto>>> GetMyPropertiesAsync(PropertyFilterDto filterDto);

}