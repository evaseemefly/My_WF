﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HM13OA.IService
{
    public partial interface IBaseService<T>
        where T:class 
    {
        bool Add(T t);
        bool Edit(T t);
        bool Remove(T t);
        bool Remove(int id);
        bool Remove(int[] ids);

        T GetById(int id);
        IQueryable<T> GetList(Expression<Func<T, bool>> whereLambda);
        IQueryable<T> GetPageList<TKey>(Expression<Func<T,bool>> whereLambda,Expression<Func<T,TKey>> orderLambda,int pageSize,int pageIndex,out int counter);
    }
}