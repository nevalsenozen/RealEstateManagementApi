using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Dto;
using RealEstateManagement.Data.Abstract;
using RealEstateManagement.Entity.Concrete;

namespace RealEstateManagement.Business.Concrete
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PropertyType> _propertyTypeRepository;
        private readonly IMapper _mapper;

        public PropertyTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _propertyTypeRepository = _unitOfWork.GetRepository<PropertyType>();
            _mapper = mapper;
        }

        public async Task<ResponseDto<PropertyTypeCreateDto>> CreateAsync(PropertyTypeCreateDto propertyTypeCreateDto)
        {
            try
            {
                var propertyType = _mapper.Map<PropertyType>(propertyTypeCreateDto);
                await _propertyTypeRepository.AddAsync(propertyType);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<PropertyTypeCreateDto>.Fail("Database error occurred while creating property type", StatusCodes.Status500InternalServerError);
                }

                var dto = _mapper.Map<PropertyTypeCreateDto>(propertyType);
                return ResponseDto<PropertyTypeCreateDto>.Success(dto, StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return ResponseDto<PropertyTypeCreateDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<PropertyTypeCreateDto>> GetAsync(int id)
        {
            try
            {
                var propertyType = await _propertyTypeRepository.GetAsync(id);
                if (propertyType is null)
                {
                    return ResponseDto<PropertyTypeCreateDto>.Fail("Property type not found", StatusCodes.Status404NotFound);
                }

                var dto = _mapper.Map<PropertyTypeCreateDto>(propertyType);
                return ResponseDto<PropertyTypeCreateDto>.Success(dto, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<PropertyTypeCreateDto>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<IEnumerable<PropertyTypeCreateDto>>> GetAllAsync()
        {
            try
            {
                var propertyTypes = await _propertyTypeRepository.GetAllAsync();
                var dtos = _mapper.Map<IEnumerable<PropertyTypeCreateDto>>(propertyTypes);
                return ResponseDto<IEnumerable<PropertyTypeCreateDto>>.Success(dtos, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<IEnumerable<PropertyTypeCreateDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<PagedResultDto<PropertyTypeCreateDto>>> GetAllPagedAsync(PaginationQueryDto paginationQueryDto)
        {
            try
            {
                var skip = (paginationQueryDto.PageNumber - 1) * paginationQueryDto.PageSize;
                var (data, totalCount) = await _propertyTypeRepository.GetPagedAsync(skip: skip, take: paginationQueryDto.PageSize);
                var dtos = _mapper.Map<IEnumerable<PropertyTypeCreateDto>>(data);
                var pagedResult = new PagedResultDto<PropertyTypeCreateDto>(dtos, paginationQueryDto.PageNumber, paginationQueryDto.PageSize, totalCount);
                return ResponseDto<PagedResultDto<PropertyTypeCreateDto>>.Success(pagedResult, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<PagedResultDto<PropertyTypeCreateDto>>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(int id, PropertyTypeUpdateDto propertyTypeUpdateDto)
        {
            try
            {
                var propertyType = await _propertyTypeRepository.GetAsync(id);
                if (propertyType is null)
                {
                    return ResponseDto<NoContent>.Fail("Property type not found", StatusCodes.Status404NotFound);
                }

                _mapper.Map(propertyTypeUpdateDto, propertyType);
                _propertyTypeRepository.Update(propertyType);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<NoContent>.Fail("Database error occurred while updating property type", StatusCodes.Status500InternalServerError);
                }

                return ResponseDto<NoContent>.Success(new NoContent(), StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return ResponseDto<NoContent>.Fail(ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(int id)
        {
            try
            {
                var propertyType = await _propertyTypeRepository.GetAsync(id);
                if (propertyType is null)
                {
                    return ResponseDto<NoContent>.Fail("Property type not found", StatusCodes.Status404NotFound);
                }

                _propertyTypeRepository.Remove(propertyType);
                var result = await _unitOfWork.SaveAsync();

                if (result < 1)
                {
                    return ResponseDto<NoContent>.Fail("Database error occurred while deleting property type", StatusCodes.Status500InternalServerError);
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
