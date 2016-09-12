using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lshOA.DALSQLSer
{
    public class BaseDAL<T>where T :class,new()
    {
        /// <summary>
        /// 通过 线程内唯一 方法 创建在同一个线程中只创建一次的数据上下文对象
        /// </summary>
        DbContext Db = DBContextFactory.GetDBContext();

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda)
        {
           //List<T> list= Db.Set<T>().Where(whereLambda).ToList();
            return Db.Set<T>().Where<T>(whereLambda);

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
            return Db.Set<T>().Where<T>(whereLambda).OrderBy(orderLambda);
        }

        public IQueryable<T> GetPageList<TKey>(int pageIndex,int pageSize,out int totalCount,Expression<Func<T,bool>> whereLambda,Expression<Func<T,TKey>>orderbyLambda,bool isAsc)
        {
            //1 根据条件查询
            var list = Db.Set<T>().Where(whereLambda);
            //2 获取集合总数
            totalCount = list.Count();

            //3 判断升序降序，
            if(isAsc)
            {
                // .orderBy 升序排列
                // .Skip 跳过一定数量的元素，返回剩余元素
                // .Take 从序列的开头返回指定数量的连续元素
                list = list.OrderBy<T, TKey>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                list = list.OrderByDescending<T, TKey>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return list;
        }

        /// <summary>
        /// 根据指定实体删除在数据库中删除指定对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Del(T model)
        {
            //1 将实体对象添加到上下文中
            Db.Set<T>().Attach(model);
            //2 将该对象的状态修改为删除状态
            Db.Set<T>().Remove(model);
            //3 不执行执行修改操作 统一执行
            return true;
            //return Db.SaveChanges();
        }

        /// <summary>
        /// 批量删除操作
        /// </summary>
        /// <param name="delWhere"></param>
        /// <returns></returns>
        public bool DelBy(Expression<Func<T, bool>> delWhere)
        {
            List<T> listDel = Db.Set<T>().Where(delWhere).ToList();
            //遍历List中的所有对象，每个对象均执行删除操作
            listDel.ForEach(u =>
            {
                Db.Set<T>().Attach(u);       //添加到EF容器
                Db.Set<T>().Attach(u);       //标识为 删除 状态
            });
            return true;
        }

        //public bool LogicDel(T model)
        //{
        //    this.Del(model);
            
        //}

        /// <summary>
        /// 根据实体为数据库中添加新的对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public T Add(T model)
        {
            Db.Set<T>().Add(model);
            return model;
        }

        /// <summary>
        /// 向数据库中添加指定集合中的全部对象
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool AddBy(List<T> list)
        {
            list.ForEach(u =>
            {
                Db.Set<T>().Add(u);
            });
            return true;

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Modify(T model)
        {

            Db.Entry<T>(model).State = System.Data.Entity.EntityState.Modified;
            return true;
        }

        public bool ModifyByList(List<T> list)
        {
            foreach (var item in list)
            {
                Db.Entry<T>(item).State = EntityState.Modified;                
            }
            return true;
        }
    }
}
