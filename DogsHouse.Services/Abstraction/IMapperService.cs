namespace DogsHouse.Services.Abstraction
{
    public interface IMapperService<Entity, DTO>
        where Entity : class
        where DTO : class
    {
        public Entity MapEntity(DTO dto);
        public Task<Entity> MapEntityAsync(DTO dto);
        public DTO MapDto(Entity entity);
        public Task<DTO> MapDtoAsync(Entity entity);
        public IEnumerable<Entity> MapEntities(IEnumerable<DTO> dtos);
        public Task<IEnumerable<Entity>> MapEntitiesAsync(IEnumerable<DTO> dtos);
        public IEnumerable<DTO> MapDtos(IEnumerable<Entity> entities);
        public Task<IEnumerable<DTO>> MapDtosAsync(IEnumerable<Entity> entities);
    }
}
