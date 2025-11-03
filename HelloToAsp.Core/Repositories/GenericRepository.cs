using AutoMapper;
using AutoMapper.QueryableExtensions;
using HelloToAsp.Core.Contracts;
using HelloToAsp.Core.Dtos;
using Microsoft.EntityFrameworkCore;
using HelloToAsp.Data.DB;

namespace HelloToAsp.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ToDoListContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(ToDoListContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<PaginationResponseDto<TResult>> GetAllAsync<TResult>(PaginationRequestDto paginationDto)
        {
            var totalSize = await _context.Set<T>().CountAsync();

            var items = await _context.Set<T>()
                .Skip((paginationDto.Page - 1) * paginationDto.Size)
                .Take(paginationDto.Size)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginationResponseDto<TResult>
            {
                Records = items,
                Size = paginationDto.Size,
                TotalCount = totalSize,
                CurrentPage = paginationDto.Page,
                TotalPage = (int)Math.Ceiling((double)totalSize / paginationDto.Size)
            };
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
