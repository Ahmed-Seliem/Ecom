using AutoMapper;
using Ecom.CORE.DTO;
using Ecom.CORE.Entities.Product;

namespace Ecom.API.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
                CreateMap<CategoryDTO,Category>().ReverseMap();
                CreateMap<UpdateCategoryDTO,Category>().ReverseMap();
        }
    }
}
