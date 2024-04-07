using Microsoft.EntityFrameworkCore;
using TL.Contracts.Repositories;
using TL.Repositories.Configurations;

namespace TL.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly CatalogContext _context;
 
        public BaseRepository(CatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public virtual int SaveChanges() {
            return _context.SaveChanges();  
        }
    }
}
