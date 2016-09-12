using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lshOA.IDAL;
using lshOA.DALSQLSer;
using System.Data.Entity.Validation;

namespace lshOA.DALFactory
{
    public partial class DBSession: IDBSession
    {
        public DbContext Db
        {
            //取得EF上下文对象
            get { return DBContextFactory.GetDBContext(); }
        }
        public bool SaveChanges()
        {
            try
            {
                return Db.SaveChanges() > 0;
            }
            catch (DbEntityValidationException dbEx)
            {

                return false;
            }
           
        }

    }
}
