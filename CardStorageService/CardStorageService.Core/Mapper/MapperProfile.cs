using AutoMapper;
using CardStorageService.Core.Models;
using CardStorageService.Storage.Models;

namespace CardStorageService.Core.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AccountDto, Account>();
            CreateMap<Card, CardDto>();
            CreateMap<Client, ClientDto>();
        }
    }
}
