    using DAL.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace DAL.DAOs
    {
        public class GenericDAO<T> : IGenericDAO<T> where T : class
        {
            private readonly Prn222Group5Context _context;
            private readonly DbSet<T> _dbSet;

            public GenericDAO(Prn222Group5Context context)
            {
                _context = context;
                _dbSet = context.Set<T>(); // Tự động lấy DbSet<T> tương ứng
            }

            public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();

            public async Task<T> GetById(int id) => await _dbSet.FindAsync(id);

            public async Task Add(T entity)
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task Update(T entity)
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }

            public async Task Delete(int id)
            {
                var entity = await GetById(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            public async Task<double> GetAverageRatingByProductId(int productId)
            {
            var average = _context.Feedbacks
                .Where(f => f.ProductId == productId)
                .Select(f => (double?)f.Rating) // Chuyển về nullable để tránh lỗi khi không có feedback nào
                .Average() ?? 0;

                return average;
        }
    }
}