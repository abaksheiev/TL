namespace TL.Contracts.Repositories
{
    public interface IBookRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
    }
}
