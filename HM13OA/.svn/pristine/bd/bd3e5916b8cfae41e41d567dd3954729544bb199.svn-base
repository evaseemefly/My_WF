using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HM13OA.DalFactory;
using HM13OA.IDAL;

namespace HM13OA.Service
{
    public abstract partial class BaseService<T>
        where T:class
    {
        protected IDbSession dbSession=DbSessionFactory.GetDbSession();

        protected IBaseDal<T> curDal; 
        protected abstract IBaseDal<T> GetDal();

        public BaseService()
        {
            curDal=GetDal();
        }

        public bool Add(T t)
        {
            curDal.Add(t);
            return dbSession.SaveChanges() > 0;
        }

        public bool Edit(T t)
        {
            curDal.Edit(t);
            return dbSession.SaveChanges()>0;
        }

        public bool Remove(int id)
        {
            curDal.Remove(id);
            return dbSession.SaveChanges() > 0;
        }

        public bool Remove(T t)
        {
            curDal.Remove(t);
            return dbSession.SaveChanges() > 0;
        }

        public bool Remove(int[] ids)
        {
            curDal.Remove(ids);
            return dbSession.SaveChanges() > 0;
        }

        public T GetById(int id)
        {
            return curDal.GetById(id);
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return curDal.GetList(whereLambda);
        }

        public IQueryable<T> GetPageList<TKey>(Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, TKey>> orderLambda, int pageSize, int pageIndex,out int counter)
        {
            return curDal.GetPageList<TKey>(whereLambda, orderLambda, pageSize, pageIndex,out counter);
        } 
    }
}
