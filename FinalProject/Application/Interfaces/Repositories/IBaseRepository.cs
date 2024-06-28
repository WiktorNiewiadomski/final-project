namespace Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity, TCreateDto, TUpdateDto>
    {
        TEntity GetById(int id);
        List<TEntity> List();
        TEntity Create(TCreateDto dto);
        TEntity Update(TUpdateDto dto);
        void DeleteById(int id);

    }
}
