using BoreWellManager.Data.Context;
using BoreWellManager.Data.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        // DI yapıyoruz
        private readonly BoreWellManagerDbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(BoreWellManagerDbContext db)
        {
            _db = db;
            _dbSet= db.Set<TEntity>();// hangi entity üzerinde çalışıyorsak onu alıyoruz.
        }
        public void Add(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            _db.Add(entity);
        }

        public void Delete(TEntity entity, bool softDelete=true)
        {
            if (softDelete)
            {
                entity.ModifiedDate = DateTime.Now;
                entity.IsDeleted = true;
                _db.Update(entity);
            }
            else
            {
                _dbSet.Remove(entity);
            }
        }

        public void DeleteById(int id)
        {
            var entity = _dbSet.FirstOrDefault(x => x.Id == id);
            Delete(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression is null ? _dbSet: _dbSet.Where(expression);
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);

        }

        public void Update(TEntity entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            _db.SaveChanges();

        }
    }
}
