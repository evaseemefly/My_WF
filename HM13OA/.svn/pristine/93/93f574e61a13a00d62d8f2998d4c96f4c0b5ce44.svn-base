using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HM13OA.IDAL
{
    public partial interface IBaseDal<T>
    {
        int Add(T t);
        int Edit(T t);
        int Remove(T t);
        int Remove(int id);
        int Remove(int[] ids);

        T GetById(int id);
        IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetPageList<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda,
            int pageSize, int pageIndex,out int counter);
    }
}
