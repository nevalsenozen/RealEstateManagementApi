using AutoMapper;
using RealEstateManagement.Data;
using RealEstateManagement.Entity.Concrete;
using RealEstateManagement.Business.Dto;
using Microsoft.EntityFrameworkCore;


namespace RealEstateManagement.Business.Concrete
{
    public class PropertyService
    {
        private readonly RealEstateManagementDbContext _context;
        private readonly IMapper _mapper;

        public PropertyService(RealEstateManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

      
        public async Task<Property> CreatePropertyAsync(PropertyCreateDto dto)
        {
            var property = _mapper.Map<Property>(dto); 
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property;
        }

        
        public async Task<List<Property>> GetAllPropertiesAsync()
        {
            return await _context.Properties.ToListAsync();
        }
    }
}
