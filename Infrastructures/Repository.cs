using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Enterprise_E_Commerce_Management_System.Infrastructures
{
    /// <summary>
    /// Uses deferred execution: the database query is not executed
    /// until the final query is fully composed and enumerated.
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly CommerceDbContext _context;

        public Repository(CommerceDbContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Set<T>()
                .AnyAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public void Update(T entity)
        {
            _context.Update(entity);
        }
        public async Task<T> GetAsReadOnlyByIdAsync(
            int id,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(
                e => EF.Property<int>(e, "Id") == id
            );
        }
        public async Task<T> GetAsReadOnlyByIdAsync(
            string id,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(
                e => EF.Property<string>(e, "Id") == id
            );
        }
        public async Task<T> GetByIdAsync(
          int id,
          params Expression<Func<T, object>>[] includes)

        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(
                e => EF.Property<int>(e, "Id") == id
            );
        }
        public async Task<T> GetByIdAsync(
          string id,
          params Expression<Func<T, object>>[] includes)

        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(
                e => EF.Property<string>(e, "Id") == id
            );
        }
        #region ReadList
        //public async Task<ICollection<T>> FindAllAsync(
        //    Expression<Func<T, bool>> filter,
        //    params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query = _context.Set<T>();

        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }

        //    return await query
        //        .AsNoTracking()
        //        .Where(filter)
        //        .ToListAsync();
        //}

        //public async Task<ICollection<T>> GetAllAsNoTrackingAsync(
        //    params Expression<Func<T, object>>[] includes)
        //{
        //    IQueryable<T> query = _context.Set<T>();

        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }

        //    return await query.AsNoTracking().ToListAsync();
        //}
        #endregion

    }
}

