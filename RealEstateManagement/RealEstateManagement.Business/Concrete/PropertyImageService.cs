using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Business.Abstract;
using RealEstateManagement.Business.Dto;
using RealEstateManagement.Data;
using RealEstateManagement.Entity.Concrete;

namespace RealEstateManagement.Business.Concrete
{
    public class PropertyImageService : IPropertyImageService
    {
        private readonly RealEstateManagementDbContext _context;
        private readonly IMapper _mapper;

        public PropertyImageService(RealEstateManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<PropertyImageDto>>> GetPropertyImagesAsync(int propertyId)
        {
            var images = await _context.PropertyImages
                .Where(x => x.PropertyId == propertyId)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();

            var dto = _mapper.Map<List<PropertyImageDto>>(images);

            return ResponseDto<List<PropertyImageDto>>.Success(dto, "Resimleri alındız", 200);
        }

        public async Task<ResponseDto<PropertyImageDto>> AddPropertyImageAsync(
            int propertyId,
            PropertyImageCreateDto dto)
        {
            var image = _mapper.Map<PropertyImage>(dto);
            image.PropertyId = propertyId;

            _context.PropertyImages.Add(image);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<PropertyImageDto>(image);

            return ResponseDto<PropertyImageDto>.Success(resultDto, "Resim başarıyla eklendi", 201);
        }

        public async Task<ResponseDto<PropertyImageDto>> UpdatePropertyImageAsync(
            int propertyId,
            int imageId,
            PropertyImageUpdateDto dto)
        {
            var image = await _context.PropertyImages
                .FirstOrDefaultAsync(x => x.Id == imageId && x.PropertyId == propertyId);

            if (image == null)
                return ResponseDto<PropertyImageDto>.Fail("Resim bulunamadı", statusCode: 404);

            _mapper.Map(dto, image);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<PropertyImageDto>(image);

            return ResponseDto<PropertyImageDto>.Success(resultDto, "Resim başarıyla güncellendi", 200);
        }

        public async Task<ResponseDto<bool>> DeletePropertyImageAsync(int propertyId, int imageId)
        {
            var image = await _context.PropertyImages
                .FirstOrDefaultAsync(x => x.Id == imageId && x.PropertyId == propertyId);

            if (image == null)
                return ResponseDto<bool>.Fail("Resim bulunamadı", statusCode: 404);

            _context.PropertyImages.Remove(image);
            await _context.SaveChangesAsync();

            return ResponseDto<bool>.Success(true, "Resim başarıyla silindi", 200);
        }
    }
}
