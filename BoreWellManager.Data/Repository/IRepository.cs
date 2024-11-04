using BoreWellManager.Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
         void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity,bool softDelete= true);
        void DeleteById(int id);    
        TEntity GetById(int id);
        TEntity Get(Expression<Func<TEntity, bool>> expression);// bu sayede Get(x=>x.Name) gibi bir arama yapabiliriz.
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null);// bir LINQ girilmediyse tüm tabloyu getir
        
    }
}
