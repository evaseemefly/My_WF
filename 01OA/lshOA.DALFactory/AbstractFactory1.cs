
 using System.Configuration;
using System.Reflection;
using lshOA.IDAL;

namespace lshOA.DALFactory
{
	public partial class AbstractFactory
     {
		public static IActionInfoDAL CreateActionInfoDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".ActionInfoDAL";
           return CreateInstance(fullClassName) as IActionInfoDAL;            
        }
			public static IBookDAL CreateBookDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".BookDAL";
           return CreateInstance(fullClassName) as IBookDAL;            
        }
			public static IDepartmentDAL CreateDepartmentDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".DepartmentDAL";
           return CreateInstance(fullClassName) as IDepartmentDAL;            
        }
			public static IFileInfoDAL CreateFileInfoDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".FileInfoDAL";
           return CreateInstance(fullClassName) as IFileInfoDAL;            
        }
			public static IOrderInfoDAL CreateOrderInfoDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".OrderInfoDAL";
           return CreateInstance(fullClassName) as IOrderInfoDAL;            
        }
			public static IR_UserInfo_ActionInfoDAL CreateR_UserInfo_ActionInfoDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".R_UserInfo_ActionInfoDAL";
           return CreateInstance(fullClassName) as IR_UserInfo_ActionInfoDAL;            
        }
			public static IRoleInfoDAL CreateRoleInfoDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".RoleInfoDAL";
           return CreateInstance(fullClassName) as IRoleInfoDAL;            
        }
			public static IsysdiagramsDAL CreatesysdiagramsDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".sysdiagramsDAL";
           return CreateInstance(fullClassName) as IsysdiagramsDAL;            
        }
			public static IUserInfoDAL CreateUserInfoDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".UserInfoDAL";
           return CreateInstance(fullClassName) as IUserInfoDAL;            
        }
			public static IWF_InstanceDAL CreateWF_InstanceDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".WF_InstanceDAL";
           return CreateInstance(fullClassName) as IWF_InstanceDAL;            
        }
			public static IWF_StepInfoDAL CreateWF_StepInfoDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".WF_StepInfoDAL";
           return CreateInstance(fullClassName) as IWF_StepInfoDAL;            
        }
			public static IWF_TempDAL CreateWF_TempDAL()
        {
            //获取类的全名称：命名空间+类名
            //使用反射创建注意需要添加该类库的引用
           string fullClassName = NameSpace + ".WF_TempDAL";
           return CreateInstance(fullClassName) as IWF_TempDAL;            
        }
		}
}