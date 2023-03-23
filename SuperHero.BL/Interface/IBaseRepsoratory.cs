
using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FRYMA_SuperHero.BL.Interface
{
    public interface IBaseRepsoratory<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> FindAsync(string includes = null);
        Task<IEnumerable<T>> FindAsync(string include1 = null, string include2 = null);
        Task<IEnumerable<T>> FindAsync(string include1 = null, string include2 = null, string include3 = null);
        Task<IEnumerable<T>> FindAsync(string include1 = null, string include2 = null, string include3 = null, string include4 = null);
        Task<IEnumerable<T>> FindAsync(string include1 = null, string include2 = null, string include3 = null, string include4 = null, string include5 = null);
        Task<T> GetByID(string id);
        Task<T> GetByID(int id);
        Task<T> GetByEmail(string Email);
        Task Create(T obj);
        Task Delete(string id);
        Task Delete(int id);
        Task Delete(T model);
        Task Update(T entity);

    }
}
