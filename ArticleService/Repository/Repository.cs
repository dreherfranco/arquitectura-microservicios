using System.Linq.Expressions;
using CategoryService.DbConfiguration;
using Microsoft.EntityFrameworkCore;

namespace ArticleService.Repository
{
    public class Repository<T> where T : class
    {
        protected AppDbContext Context { get; set; }
        public Repository(AppDbContext context)
        {
            Context = context;
        }

        public async Task<T> Add(T entity)
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return this.Context.Set<T>().AsQueryable();
            }
            else
            {
                return this.Context.Set<T>().Where(filter);

            }

        }

        public async Task Delete(T entity)
        {
            this.Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Update(T entidad)
        {
            this.Context.Entry(entidad).State = EntityState.Modified;
            await this.Context.SaveChangesAsync();
        }

    }
}