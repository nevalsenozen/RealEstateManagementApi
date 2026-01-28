using AutoMapper;
using RealEstateManagement.Data;
using RealEstateManagement.Entity.Concrete;
using RealEstateManagement.Business.Dto;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Business.Abstract;
using System.Linq.Expressions;



namespace RealEstateManagement.Business.Concrete
{
    public class PropertyService : IPropertyService
    {
        private readonly RealEstateManagementDbContext _context;
        private readonly IMapper _mapper;

        public PropertyService(RealEstateManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<ResponseDto<PropertyCreateDto>> CreateAsync(PropertyCreateDto propertyCreateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Property> CreatePropertyAsync(PropertyCreateDto dto)
        {
            var property = _mapper.Map<Property>(dto); 
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public Task<ResponseDto<IEnumerable<PropertyCreateDto>>> GetAllAsync(Expression<Func<Microsoft.EntityFrameworkCore.Metadata.Internal.Property, bool>>? predicate, Func<IQueryable<Microsoft.EntityFrameworkCore.Metadata.Internal.Property>, IOrderedQueryable<Microsoft.EntityFrameworkCore.Metadata.Internal.Property>>? orderBy, bool? includeCategories = null, int? categoryId = null, bool? isDeleted = null)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<PagedResultDto<PropertyCreateDto>>> GetAllPagedAsync(PaginationQueryDto paginationQueryDto, Expression<Func<Microsoft.EntityFrameworkCore.Metadata.Internal.Property, bool>>? predicate = null, Func<IQueryable<Microsoft.EntityFrameworkCore.Metadata.Internal.Property>, IOrderedQueryable<Microsoft.EntityFrameworkCore.Metadata.Internal.Property>>? orderBy = null, bool? includeCategories = false, int? categoryId = null, bool? isDeleted = null)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await _context.Properties.ToListAsync();
        }

        public Task<ResponseDto<PropertyCreateDto>> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<List<PropertyCreateDto>>> GetMyPropertiesAsync(PropertyFilterDto filterDto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<NoContent>> SoftDeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<NoContent>> UpdateAsync(int id, PropertyUpdateDto propertyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
