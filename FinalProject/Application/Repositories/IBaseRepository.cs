namespace Application.Repositories
{
    public interface IBaseRepository<TEntity, TCreateDto, TUpdateDto>
    {
        TEntity GetById(int id);
        TEntity[] List();
        TEntity Create(TCreateDto dto);
        TEntity Update(TUpdateDto dto);
        void DeleteById(int id);

    }
}
