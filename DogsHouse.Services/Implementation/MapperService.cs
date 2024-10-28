using AutoMapper;
using DogsHouse.Services.Abstraction;

namespace DogsHouse.Services
{
    public class MapperService<Entity, DTO> : IMapperService<Entity, DTO>
        where Entity : class
        where DTO : class
    {
        private readonly IMapper mapper;

        public MapperService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public DTO MapDto(Entity entity)
        {
            return mapper.Map<DTO>(entity);
        }

        public async Task<DTO> MapDtoAsync(Entity entity)
        {
            return await Task.FromResult(MapDto(entity));
        }

        public IEnumerable<DTO> MapDtos(IEnumerable<Entity> entities)
        {
            return mapper.Map<IEnumerable<DTO>>(entities);
        }

        public async Task<IEnumerable<DTO>> MapDtosAsync(IEnumerable<Entity> entities)
        {
            return await Task.FromResult(MapDtos(entities));
        }

        public IEnumerable<Entity> MapEntities(IEnumerable<DTO> dtos)
        {
            return mapper.Map<IEnumerable<Entity>>(dtos);
        }

        public async Task<IEnumerable<Entity>> MapEntitiesAsync(IEnumerable<DTO> dtos)
        {
            return await Task.FromResult(MapEntities(dtos));
        }

        public Entity MapEntity(DTO dto)
        {
            return mapper.Map<Entity>(dto);
        }

        public async Task<Entity> MapEntityAsync(DTO dto)
        {
            return await Task.FromResult(MapEntity(dto));
        }
    }
}
