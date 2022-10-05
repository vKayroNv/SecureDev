using AutoMapper;
using CardStorageService.API.Models.Requests;
using CardStorageService.Core.Models;
using CardStorageService.Storage.Models;

namespace CardStorageService.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CardCreateRequest, Card>();
            CreateMap<CardUpdateRequest, Card>();

            CreateMap<ClientCreateRequest, Client>();
            CreateMap<ClientUpdateRequest, Client>();

            CreateMap<AuthRegisterRequest, AccountDto>();
        }
    }
}
