using System.Linq.Expressions;

namespace Enterprise_E_Commerce_Management_System.Infrastructures
{
    /// <summary>
    /// EF Read Methods are for Simple Queries
    /// </summary> 
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteByIdAsync(int id);
        Task<bool> ExistsByIdAsync(int id);
        Task<T> GetByIdAsync(
         int id,
         params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(
          string id,
          params Expression<Func<T, object>>[] includes);
        Task<T> GetAsReadOnlyByIdAsync(
          int id,
          params Expression<Func<T, object>>[] includes);
        Task<T> GetAsReadOnlyByIdAsync(
            string id,
            params Expression<Func<T, object>>[] includes);
        
            #region ReadList
            //Task<ICollection<T>> FindAllAsync(
            //    Expression<Func<T, bool>> filter,
            //    params Expression<Func<T, object>>[] includes
            //);

            //Task<ICollection<T>> GetAllAsNoTrackingAsync(
            //    params Expression<Func<T, object>>[] includes
            //); 
            #endregion
        }
}
