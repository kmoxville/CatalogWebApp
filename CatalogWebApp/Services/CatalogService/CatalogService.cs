using AutoMapper;
using CatalogWebApp.Services.EmailService;
using CatalogWebApp.ViewModels.Catalog;

namespace CatalogWebApp.Services.CatalogService
{
    public class CatalogService : ICatalogService
    {
        private readonly CatalogContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CatalogService(CatalogContext context, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<bool> Create(CatalogViewModel catalogViewModel)
        {
            var catalogModel = _mapper.Map<ProductModel>(catalogViewModel);

            if (catalogModel.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    catalogModel.ImageFile.CopyTo(memoryStream);
                    catalogModel.Image = new ProductImageModel()
                    {
                        Image = memoryStream.ToArray()
                    };
                }
            }

            _context.Add(catalogModel);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            try
            {
                await _emailService.SendEmailAsync(
                    email: "alex.samylovskikh@gmail.com", //asp2022gb@rodion-m.ru
                    message: $"Created new product {catalogModel.Name}",
                    subject: "New product");
            }
            catch (Exception)
            {
                // ignore
            }

            return true;
        }

        public async Task<bool> Delete(int? id)
        {
            var catalogModel = await _context.Catalogs.FindAsync(id);

            if (catalogModel != null)
            {
                _context.Catalogs.Remove(catalogModel);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<CatalogViewModel> Details(int? id)
        {
            return _mapper.Map<CatalogViewModel>(await _context.Catalogs.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<byte[]?> GetImageAsync(int id)
        {
            var image = await _context.CatalogImages.FirstOrDefaultAsync(x => x.Id == id);
            if (image != null)
                return image.Image;
            else
                return null;
        }

        public async Task<CatalogIndexViewModel> IndexAsync()
        {
            return new CatalogIndexViewModel() { Products = await _context.Catalogs.ToListAsync() };
        }

        public async Task<bool> Save(CatalogViewModel catalogViewModel)
        {
            var catalogModel = _mapper.Map<ProductModel>(catalogViewModel);

            if (catalogModel.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    catalogModel.ImageFile.CopyTo(memoryStream);
                    catalogModel.Image = new ProductImageModel()
                    {
                        Image = memoryStream.ToArray()
                    };
                }
            }

            try
            {
                _context.Update(catalogModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }
    }
}
