using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using HM13OA.Model;

namespace HM13OA.DAL
{
    public partial class ContextFactory
    {
        public static DbContext GetContext()
        {
            DbContext context = CallContext.GetData("OAContext") as DbContext;
            if (context == null)
            {
                context=new HM13OAContainer();
                CallContext.SetData("OAContext",context);
            }
            return context;
        }
    }
}
