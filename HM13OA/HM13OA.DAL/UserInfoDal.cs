using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HM13OA.IDAL;
using HM13OA.Model;

namespace HM13OA.DAL
{
    public partial class UserInfoDal// : BaseDal<UserInfo>,IUserInfoDal
    {
        public int SetRole(int uid, int[] rids)
        {
            UserInfo userInfo = GetById(uid);

            userInfo.RoleInfo.Clear();

            RoleInfoDal roleInfoDal=new RoleInfoDal();
            foreach (var rid in rids)
            {
                userInfo.RoleInfo.Add(roleInfoDal.GetById(rid));
            }

            return 0;
        }

        public int SetAction(int uid, int aid, int allow)
        {
            //根据编号找到用户
            UserInfo userInfo = GetById(uid);

            //通过导航属性判断是否已经有这个权限
            UserAction ua1 = userInfo.UserAction.Where(ua => ua.ActionId == aid).FirstOrDefault();

            //进行否决判断
            if (ua1 != null)
            {
                if (allow == 1)
                {
                    ua1.IsAllow = true;
                }else if (allow == -1)
                {
                    ua1.IsAllow = false;
                }
                else
                {
                    userInfo.UserAction.Remove(ua1);
                }
            }
            else
            {
                if (allow != 0)
                {
                    ua1 = new UserAction()
                    {
                        UserId = uid,
                        ActionId = aid
                    };
                    if (allow == 1)
                    {
                        ua1.IsAllow = true;
                    }
                    else if (allow == -1)
                    {
                        ua1.IsAllow = false;
                    }
                    userInfo.UserAction.Add(ua1);
                }
            }

            return 0;
        }


        //    //DbContext context=new HM13OAContainer();
        //    private DbContext context = ContextFactory.GetContext();

        //    //增加
        //    public int Add(UserInfo userInfo)
        //    {
        //        context.Set<UserInfo>().Add(userInfo);
        //        return context.SaveChanges();
        //    }

        //    //修改
        //    public int Edit(UserInfo userInfo)
        //    {
        //        context.Entry(userInfo).State = EntityState.Modified;
        //        return context.SaveChanges();
        //    }

        //    //删除
        //    public int Remove(int id)
        //    {
        //        UserInfo u1 = context.Set<UserInfo>().Find(id);
        //        context.Set<UserInfo>().Remove(u1);
        //        return context.SaveChanges();
        //    }

        //    public int Remove(int[] ids)
        //    {
        //        int counter = ids.Length;
        //        for (int i = 0; i < counter; i++)
        //        {
        //            UserInfo u1 = context.Set<UserInfo>().Find(ids[i]);
        //            context.Set<UserInfo>().Remove(u1);
        //        }
        //        return context.SaveChanges();
        //    }

        //    public int Remove(UserInfo userInfo)
        //    {
        //        context.Set<UserInfo>().Remove(userInfo);
        //        return context.SaveChanges();
        //    }

        //    //查询
        //    public UserInfo GetById(int id)
        //    {
        //        return context.Set<UserInfo>().Find(id);
        //    }

        //    public IQueryable<UserInfo> GetList(Expression<Func<UserInfo, bool>> whereLambda)
        //    {
        //        return context.Set<UserInfo>().Where(whereLambda);
        //        //IEnumerable:select * from userinfo
        //        //IQueryable:select * from userinfo where ...
        //    }

        //    public IQueryable<UserInfo> GetPageList<TKey>(Expression<Func<UserInfo,bool>> whereLambda,Expression<Func<UserInfo,TKey>> orderLambda,int pageIndex,int pageSize)
        //    {
        //        return context.Set<UserInfo>().Where(whereLambda)
        //            .OrderByDescending(orderLambda)
        //            .Skip((pageIndex - 1)*pageSize)
        //            .Take(pageSize);
        //    }

        //}
    }
}
