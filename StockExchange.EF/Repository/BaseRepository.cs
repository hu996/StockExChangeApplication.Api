using StockExChange.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockExChange.EF.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class 
   
    {
        private readonly  ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public T Create(T entity)
        {
             _context.Set<T>().Add(entity);
            _context.SaveChanges(); 
            return entity;
        }

        public IEnumerable<T> FindAll()
        {
            return _context.Set<T>().ToList();
        }

        public T FindById(int Id)
        {
            return _context.Set<T>().Find(Id);
        }

        public IEnumerable<T> FindByName(Expression<Func<T, bool>>match)
        {
            //return _context.Set<T>().FirstOrDefault(Name);
            

             return _context.Set<T>().Where(match).ToList();
          
        }

        public T FindByName(string Name)
        {
            throw new NotImplementedException();
        }
    }
}
