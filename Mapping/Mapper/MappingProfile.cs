using AutoMapper;
using ModelLayer.DTO;
using RepositoryLayer.Entity;

namespace Mapping.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddressBookEntity, AddressBookDTO>().ReverseMap();
        }
    }
}
