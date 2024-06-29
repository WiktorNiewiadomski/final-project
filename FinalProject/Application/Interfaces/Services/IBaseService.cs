namespace Application.Interfaces.Services
{
    public interface IBaseService<TEntity, TCreateDto, TUpdateDto>
    {
        TEntity GetById(int id);
        List<TEntity> GetAll();
        TEntity Create(TCreateDto dto);
        TEntity Update(int id, TUpdateDto dto);
        void DeleteById(int id);
    }
}
