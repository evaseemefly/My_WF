using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace HM13OA.DAL
{
    public partial class BaseDal<T>
        where T:class 
    {
                //DbContext context=new HM13OAContainer();
        private DbContext context = ContextFactory.GetContext();

        //增加
        public int Add(T userInfo)
        {
            context.Set<T>().Add(userInfo);
            //return context.SaveChanges();
            return 0;
        }

        //修改
        public int Edit(T userInfo)
        {
            context.Entry(userInfo).State = EntityState.Modified;
            //return context.SaveChanges();
            return 0;
        }

        //删除
        public int Remove(int id)
        {
            T u1 = context.Set<T>().Find(id);
            //context.Set<T>().Remove(u1);
            //return context.SaveChanges();

            context.Entry(u1).Property("IsDelete").IsModified = true;
            context.Entry(u1).Property("IsDelete").CurrentValue = true;

            return 0;
        }

        public int Remove(int[] ids)
        {
            int counter = ids.Length;
            for (int i = 0; i < counter; i++)
            {
                //T u1 = context.Set<T>().Find(ids[i]);
                //context.Set<T>().Remove(u1);
                Remove(ids[i]);
            }
            //return context.SaveChanges();
            return 0;
        }

        public int Remove(T userInfo)
        {
            //context.Set<T>().Remove(userInfo);
            //return context.SaveChanges();

            //实现软删除
            context.Set<T>().Attach(userInfo);
            context.Entry(userInfo).Property("IsDelete").IsModified = true;
            context.Entry(userInfo).Property("IsDelete").CurrentValue = true;

            return 0;
        }

        //查询
        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
            return context.Set<T>().Where(whereLambda);
            //IEnumerable:select * from userinfo
            //IQueryable:select * from userinfo where ...
        }

        public IQueryable<T> GetPageList<TKey>(Expression<Func<T,bool>> whereLambda,Expression<Func<T,TKey>> orderLambda,int pageSize,int pageIndex,out int counter)
        {
            counter = context.Set<T>().Where(whereLambda).Count();

            return context.Set<T>().Where(whereLambda)
                .OrderByDescending(orderLambda)
                .Skip((pageIndex - 1)*pageSize)
                .Take(pageSize);
        }

    }
}
