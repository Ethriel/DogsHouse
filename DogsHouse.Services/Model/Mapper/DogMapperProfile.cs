using AutoMapper;
using DogsHouse.Database.Model;

namespace DogsHouse.Services.Model.Mapper
{
    public class DogMapperProfile : Profile
    {
        public DogMapperProfile()
        {
            CreateMap<Dog, DogDTO>();
            CreateMap<DogDTO, Dog>();
        }
    }
}
