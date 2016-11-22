using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BaseRepository<T> where T: BaseEntity, new()
    {
        private DbContext db;
        private DbSet<T> dbSet;
        private UnitOfWork unitOfWork;

        public BaseRepository()
        {
            db = new BookATableContext();
            dbSet = db.Set<T>();
            this.unitOfWork = new UnitOfWork();
        }

        public BaseRepository(UnitOfWork unitOfWork)
        {
            db = new BookATableContext();
            dbSet = db.Set<T>();
            this.unitOfWork = unitOfWork;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.Where(e => e.IsDeleted == false).ToList();
        } 
        public virtual IEnumerable<T> GetAll(Expression<Func<T,bool>>filter)
        {
            return dbSet.Where(filter).ToList();
        }
        public IEnumerable<T> GetAll(int? page = null, int pageSize = 10)
        {
            return dbSet.OrderBy(i => i.Id).Skip(page.Value * pageSize).Take(pageSize).ToList();
        }
        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }
        public virtual T Get(Expression<Func<T, bool>> filter)
        {
            return dbSet.FirstOrDefault(filter);
        }
        protected virtual void Insert(T entity)
        {
          
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                dbSet.Add(entity);
        db.SaveChanges();
            
            
            
        }
        protected virtual void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
        public virtual void Save(T entity)
        {
            if (entity.Id > 0)
            {
                Update(entity);
            }
            else
            {   
                Insert(entity);
                
            }
        }
        public virtual void Delete(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.IsDeleted = true;
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
       
    }
}
