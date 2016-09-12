using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HM13OA.DAL;
using HM13OA.IDAL;

namespace HM13OA.DalFactory
{
    public partial class DbSession//:IDbSession
    {
        //工厂
        //public IUserInfoDal GetUserInfoDal()
        //{
        //    return  new UserInfoDal();
        //}

        //事务
        public int SaveChanges()
        {
            DbContext context = ContextFactory.GetContext();
            return context.SaveChanges();
        }
    }
}
