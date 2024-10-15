using AutoMapper;
using SupleStore.DTOs;
using SupleStore.Models;

namespace SupleStore.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductoInsertDto, Productos>();

            CreateMap<Productos, ProductosDto>()
                .ForMember(dto=> dto.Id,
                            m=> m.MapFrom(p=> p.Id));
        }
    }
}
