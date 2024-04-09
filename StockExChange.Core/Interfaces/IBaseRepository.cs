using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StockExChange.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class 
    {

        T Create(T entity);
        T FindById(int Id);
        T FindByName(String Name);

        IEnumerable<T> FindAll();
        IEnumerable<T> FindByName(Expression<Func<T, bool>> match);

    }
   
}
