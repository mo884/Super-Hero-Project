using FRYMA_SuperHero.BL.Interface;
using SuperHero.DAL.Database;
using SuperHero.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace FRYMA_SuperHero.BL.Reposoratory
{
    public class BaseRepsoratory<T> : IBaseRepsoratory<T> where T : class
    {
        #region Prop
        protected ApplicationContext Db;
        #endregion

        #region Ctor
        public BaseRepsoratory(ApplicationContext Db)
        {
            this.Db = Db;
        }
        #endregion

        #region Create
        public async Task Create(T obj)
        {
            await Db.Set<T>().AddAsync(obj);
            await Db.SaveChangesAsync();
        }
        #endregion

        #region Delete
        public async Task Delete(string id)
        {
            var oldDate = await Db.Set<T>().FindAsync(id);
            Db.Set<T>().Remove(oldDate);
            await Db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var oldDate = await Db.Set<T>().FindAsync(id);
            Db.Set<T>().Remove(oldDate);
            await Db.SaveChangesAsync();
        }
        public async Task Delete(T model)
        {
            Db.Set<T>().Remove(model);
            await Db.SaveChangesAsync();
        }
       

        #endregion

        #region GetAll With out Include
        public async Task<IEnumerable<T>> GetAll()
        {
            //var data = await Db.Set<T>().Include("Group").Include("District").ToListAsync();
            var data = await Db.Set<T>().ToListAsync();
            return data;
        }
        #endregion

        #region Get By Email
        public async Task<T> GetByEmail(string Email)
        {

            var data = await Db.Set<T>().FindAsync(Email);
            return data;
        }
        #endregion

        #region Get By Id
        public async Task<T> GetByID(string id)
        {
            var data = await Db.Set<T>().FindAsync(id);
            return data;
        } 
        public async Task<T> GetByID(int id)
        {
            var data = await Db.Set<T>().FindAsync(id);
            return data;
        }


        #endregion

        #region Include
            #region GetAll With One Include
            public async Task<IEnumerable<T>> FindAsync(string include = null)
            {
                return await Db.Set<T>().Include(include).ToListAsync();

            }
     
        #endregion

            #region GetAll With Two Include
        public async Task<IEnumerable<T>> FindAsync(string include1 = null, string include2 = null)
            {
                return await Db.Set<T>().Include(include1).Include(include2).ToListAsync();
            }
            #endregion

            #region GetAll With three Include
            public async Task<IEnumerable<T>> FindAsync(string include1 = null, string include2 = null, string include3 = null)
            {
                return await Db.Set<T>().Include(include1).Include(include2).Include(include3).ToListAsync();
            }
            #endregion

            #region GetAll With four Include
            public async Task<IEnumerable<T>> FindAsync(string include1 = null, string include2 = null, string include3 = null, string include4 = null)
            {
                return await Db.Set<T>().Include(include1).Include(include2).Include(include3).Include(include4).ToListAsync();
            }
            #endregion

            #region GetAll With five Include
            public async Task<IEnumerable<T>> FindAsync(string include1 = null, string include2 = null, string include3 = null, string include4 = null, string include5 = null)
            {
                return await Db.Set<T>().Include(include1).Include(include2).Include(include3).Include(include4).Include(include5).ToListAsync();
            }
            #endregion
        #endregion


        #region Update
        public async Task Update(T entity)
        {
            Db.Set<T>().Update(entity);
            Db.SaveChanges();
            
        }

       



        #endregion



    }
}
