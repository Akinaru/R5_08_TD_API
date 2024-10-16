using AutoMapper;
using TD1.Models.DTO;
using TD1.Models.EntityFramework;

namespace TD1.Models.Mapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile() {
            CreateMap<ProduitDto, Produit>().ReverseMap()
                 .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TypeProduitNavigation.nomTypeProduit))
                 .ForMember(dest => dest.Marque, opt => opt.MapFrom(src => src.MarqueNavigation.nomMarque));
  }

    }
}
