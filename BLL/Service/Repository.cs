using BLL.Interface;
using DAL.DAOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IGenericDAO<T> _dao;

        public Repository(IGenericDAO<T> dao)
        {
            _dao = dao;
        }

        public async Task<IEnumerable<T>> GetAll() => await _dao.GetAll();
        public async Task<T> GetById(int id) => await _dao.GetById(id);
        public async Task Add(T entity) => await _dao.Add(entity);
        public async Task Update(T entity) => await _dao.Update(entity);
        public async Task Delete(int id) => await _dao.Delete(id);
        public async Task<double> GetAverageRatingByProductId(int productId) {
           return await _dao.GetAverageRatingByProductId(productId);
        }
    }
}
