using AutoMapper;
using RealEstateManagement.Data;
using RealEstateManagement.Entity.Concrete;
using RealEstateManagement.Business.Dto;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Business.Abstract;
using System.Linq.Expressions;
using RealEstateManagement.Data.Abstract;
using Microsoft.AspNetCore.Http;



namespace RealEstateManagement.Business.Concrete
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Property> _propertyRepository;
        private readonly IRepository<PropertyType> _propertyTypeRepository;

        public PropertyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _propertyRepository = _unitOfWork.GetRepository<Property>();
            _propertyTypeRepository = _unitOfWork.GetRepository<PropertyType>();
            _mapper = mapper;
        }

        public async Task<ResponseDto<PropertyCreateDto>> CreateAsync(PropertyCreateDto propertyCreateDto)
        {
            try
            {
                var propertyType = await _propertyTypeRepository.GetAsync(propertyCreateDto.PropertyTypeId);
                if (propertyType is null)
                {
                    return ResponseDto<PropertyCreateDto>.Fail($"PropertyType ID {propertyCreateDto.PropertyTypeId} not found", StatusCodes.Status400BadRequest);
                }

                var property = _mapper.Map<Property>(propertyCreateDto);

                await _propertyRepository.AddAsync(property);

                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<PropertyCreateDto>.Fail("Database error occurred while saving property", StatusCodes.Status500InternalServerError);
                }

                var propertyDto = _mapper.Map<PropertyCreateDto>(property);

                return ResponseDto<PropertyCreateDto>.Success(propertyDto, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return ResponseDto<PropertyCreateDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<IEnumerable<PropertyCreateDto>>> GetAllAsync(Expression<Func<Microsoft.EntityFrameworkCore.Metadata.Internal.Property, bool>>? predicate, Func<IQueryable<Microsoft.EntityFrameworkCore.Metadata.Internal.Property>, IOrderedQueryable<Microsoft.EntityFrameworkCore.Metadata.Internal.Property>>? orderBy, bool? includeCategories = null, int? categoryId = null, bool? isDeleted = null)
        {
            try
            {
                var properties = await _propertyRepository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<PropertyCreateDto>>(properties);
                return ResponseDto<IEnumerable<PropertyCreateDto>>.Success(dtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<PropertyCreateDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<PagedResultDto<PropertyCreateDto>>> GetAllPagedAsync(PaginationQueryDto paginationQueryDto, Expression<Func<Microsoft.EntityFrameworkCore.Metadata.Internal.Property, bool>>? predicate = null, Func<IQueryable<Microsoft.EntityFrameworkCore.Metadata.Internal.Property>, IOrderedQueryable<Microsoft.EntityFrameworkCore.Metadata.Internal.Property>>? orderBy = null, bool? includeCategories = false, int? categoryId = null, bool? isDeleted = null)
        {
            try
            {
                var skip = (paginationQueryDto.PageNumber - 1) * paginationQueryDto.PageSize;
                var (data, totalCount) = await _propertyRepository.GetPagedAsync(skip: skip, take: paginationQueryDto.PageSize);
                var dtos = _mapper.Map<IEnumerable<PropertyCreateDto>>(data);
                var pagedResult = new PagedResultDto<PropertyCreateDto>(dtos, paginationQueryDto.PageNumber, paginationQueryDto.PageSize, totalCount);
                return ResponseDto<PagedResultDto<PropertyCreateDto>>.Success(pagedResult, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<PagedResultDto<PropertyCreateDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<PropertyCreateDto>> GetAsync(int id)
        {
            try
            {
                var property = await _propertyRepository.GetAsync(id);
                if (property is null)
                {
                    return ResponseDto<PropertyCreateDto>
                    .Fail("Property not found", StatusCodes.Status404NotFound);
                }

                var propertyDto = _mapper.Map<PropertyCreateDto>(property);
                return ResponseDto<PropertyCreateDto>.Success(propertyDto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<PropertyCreateDto>
                .Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<List<PropertyCreateDto>>> GetMyPropertiesAsync(PropertyFilterDto filterDto)
        {
            try
            {
                var skip = (filterDto.PageNumber - 1) * filterDto.PageSize;
                var (properties, _) = await _propertyRepository.GetPagedAsync(skip: skip, take: filterDto.PageSize);
                var dtos = _mapper.Map<List<PropertyCreateDto>>(properties);
                return ResponseDto<List<PropertyCreateDto>>.Success(dtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<List<PropertyCreateDto>>
                .Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<NoContent>> SoftDeleteAsync(int id)
        {
            try
            {
                var property = await _propertyRepository.GetAsync(id);
                if (property is null)
                {
                    return ResponseDto<NoContent>.Fail("Property not found", StatusCodes.Status404NotFound);
                }

                property.IsDeleted = true;
                _propertyRepository.Update(property);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<NoContent>
                    .Fail("Database error occurred while deleting property", 
                    StatusCodes.Status500InternalServerError);
                }

                return ResponseDto<NoContent>.Success(new NoContent(), StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(int id, PropertyUpdateDto propertyUpdateDto)
        {
            try
            {
                var property = await _propertyRepository.GetAsync(id);
                if (property is null)
                {
                    return ResponseDto<NoContent>.Fail("Property not found", StatusCodes.Status404NotFound);
                }

                var propertyType = await _propertyTypeRepository.GetAsync(propertyUpdateDto.PropertyTypeId);
                if (propertyType is null)
                {
                    return ResponseDto<NoContent>.Fail($"PropertyType ID {propertyUpdateDto.PropertyTypeId} not found", StatusCodes.Status400BadRequest);
                }

                _mapper.Map(propertyUpdateDto, property);
                _propertyRepository.Update(property);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<NoContent>.Fail("Database error occurred while updating property", StatusCodes.Status500InternalServerError);
                }

                return ResponseDto<NoContent>.Success(new NoContent(), StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }
    }
}
