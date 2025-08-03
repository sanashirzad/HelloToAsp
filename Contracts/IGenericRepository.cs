using HelloToAsp.Dtos;

namespace HelloToAsp.Contracts
{
    public interface IGenericRepository<T>
    {
        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<PaginationResponseDto<TResult>> GetAllAsync<TResult>(PaginationRequestDto paginationDto);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
        Task<bool> Exists(int id);
    }
}
