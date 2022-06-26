using AutoMapper;
using CatalogWebApp.ViewModels.Catalog;

namespace CatalogWebApp.DAL
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductModel, CatalogViewModel>()
                .ReverseMap();
        }
    }
}
