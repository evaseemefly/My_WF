using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace lshOA.IBLL
{
    public interface IBaseBLL<T>where T :class,new()
    {
         IDAL.IDBSession GetCurrentDBSession { get; }

         IDAL.IBaseDAL<T> CurrentDAL { get; set; }

        IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 根据条件及排序条件查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderLambda"></param>
        /// <returns></returns>
         IQueryable<T> GetListBy<TKey>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderLambda);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页码</param>
        /// <param name="totalCount">集合总数</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">升序标记</param>
        /// <returns></returns>
        IQueryable<T> GetPageList<TKey>(int pageIndex, int pageSize, out int totalCount, Expression<Func<T, bool>> whereLambda, Expression<Func<T, TKey>> orderbyLambda, bool isAsc);

        /// <summary>
        /// 根据指定实体删除在数据库中删除指定对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Del(T model);

        /// <summary>
        /// 批量删除操作
        /// </summary>
        /// <param name="delWhere"></param>
        /// <returns></returns>
        bool DelBy(Expression<Func<T, bool>> delWhere);


        /// <summary>
        /// 根据实体为数据库中添加新的对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Add(T model);

        /// <summary>
        /// 向数据库中添加指定集合中的全部对象
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool AddBy(List<T> list);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool  Modify(T model);

       
    }
}
