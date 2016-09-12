using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lshOA.BLL
{
    public abstract class BaseBLL<T>where T:class,new()
    {
        public IDAL.IDBSession GetCurrentDBSession
        {
            get
            {
                //使用 线程内唯一 的方法创建DBSession
                return DALFactory.DBSessionFactory.CreateDbSession();
            }
        }

        public IDAL.IBaseDAL<T> CurrentDAL { get; set; }

        /// <summary>
        /// 需要由子类重写的虚方法
        /// </summary>
        public abstract void SetCurrentDAL();

        /// <summary>
        /// 父类的无参构造函数调用需要由子类重写的虚方法
        /// </summary>
        public BaseBLL()
        {
            SetCurrentDAL();
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
          return  CurrentDAL.GetList(whereLambda);
        }

        /// <summary>
        /// 根据条件及排序条件查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderLambda"></param>
        /// <returns></returns>
       public IQueryable<T> GetListBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda)
        {
            return CurrentDAL.GetListBy(whereLambda, orderLambda);
        }

        public IQueryable<T> GetPageList<TKey>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderbyLambda, bool isAsc)
        {
            return CurrentDAL.GetPageList(pageIndex, pageSize,out totalCount, whereLambda, orderbyLambda, isAsc);
        }

        /// <summary>
        /// 根据指定实体删除在数据库中删除指定对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Del(T model)
        {
             CurrentDAL.Del(model);
            return this.GetCurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 批量删除操作
        /// </summary>
        /// <param name="delWhere"></param>
        /// <returns></returns>
        public bool DelBy(Expression<Func<T, bool>> delWhere)
        {
            CurrentDAL.DelBy(delWhere);
            return this.GetCurrentDBSession.SaveChanges();
        }


        /// <summary>
        /// 根据实体为数据库中添加新的对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Add(T model)
        {
            CurrentDAL.Add(model);
             this.GetCurrentDBSession.SaveChanges();
            return model;
        }

        /// <summary>
        /// 向数据库中添加指定集合中的全部对象
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
       public bool AddBy(List<T> list)
        {
            CurrentDAL.AddBy(list);
           return this.GetCurrentDBSession.SaveChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Modify(T model)
        {
            CurrentDAL.Modify(model);
            return this.GetCurrentDBSession.SaveChanges();
        }
    }
}
