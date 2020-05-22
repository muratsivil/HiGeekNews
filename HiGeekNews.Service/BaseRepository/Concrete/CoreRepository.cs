using HiGeekNews.DAL.Context;
using HiGeekNews.Entity.Entities;
using HiGeekNews.Entity.Enums;
using HiGeekNews.Service.BaseRepository.Abstruction;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace HiGeekNews.Service.BaseRepository.Concrete
{
    public class CoreRepository<T> : ICoreRepository<T> where T : BaseEntity
    {
        private static ProjectContext _context;
        public CoreRepository()
        {
            _context = new ProjectContext();
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
            Save();
        }

        public void Add(List<T> items)
        {
            _context.Set<T>().AddRange(items);
            Save();
        }

        public bool Any(Expression<Func<T, bool>> exp) => _context.Set<T>().Any(exp);


        public List<T> GetActive()
        {
            return _context.Set<T>().Where(x => x.Status != Status.Passive).ToList();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp).FirstOrDefault();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().Where(exp).ToList();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            T item = GetById(id);
            item.Status = Status.Passive;
            item.DeleteDate = DateTime.Now;
            Save();
        }

        public void RemoveAll(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(T item)
        {
            T update = GetById(item.Id);
            item.Status = Status.Modified;
            DbEntityEntry dbEntityEntry = _context.Entry(update);
            dbEntityEntry.CurrentValues.SetValues(item);
            Save();
        }
    }
}
