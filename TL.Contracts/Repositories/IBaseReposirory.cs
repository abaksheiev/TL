namespace TL.Contracts.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get Entity based on Identificatory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// Add TEntity in ContextDB, not saved
        /// </summary>
        /// <param name="entity">Instance of model</param>
        void Add(TEntity entity);

        /// <summary>
        /// Update TEntity
        /// </summary>
        /// <param name="entity">Instance of model</param>
        void Update(TEntity entity);

        /// <summary>
        /// Remove TEntity
        /// </summary>
        void Remove(int id);

        /// <summary>
        /// Get All items of TEntity
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Save current changes
        /// </summary>
        /// <returns>Result of operation</returns>
        int SaveChanges();
    }
}
