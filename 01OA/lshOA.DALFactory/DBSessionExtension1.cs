
 
using lshOA.IDAL;

namespace lshOA.DALFactory
{

	
	/// <summary>
    /// DBSession的拓展类
    /// </summary>
    public partial class DBSession : IDAL.IDBSession
    {
	        private IDAL.IActionInfoDAL _ActionInfoDal;

        public IActionInfoDAL ActionInfoDAL
        {
            get
            {
                if (_ActionInfoDal == null)
                {
                    _ActionInfoDal = AbstractFactory.CreateActionInfoDAL();
                }
                return _ActionInfoDal;
            }

            set { _ActionInfoDal = value; }
        }
		        private IDAL.IBookDAL _BookDal;

        public IBookDAL BookDAL
        {
            get
            {
                if (_BookDal == null)
                {
                    _BookDal = AbstractFactory.CreateBookDAL();
                }
                return _BookDal;
            }

            set { _BookDal = value; }
        }
		        private IDAL.IDepartmentDAL _DepartmentDal;

        public IDepartmentDAL DepartmentDAL
        {
            get
            {
                if (_DepartmentDal == null)
                {
                    _DepartmentDal = AbstractFactory.CreateDepartmentDAL();
                }
                return _DepartmentDal;
            }

            set { _DepartmentDal = value; }
        }
		        private IDAL.IFileInfoDAL _FileInfoDal;

        public IFileInfoDAL FileInfoDAL
        {
            get
            {
                if (_FileInfoDal == null)
                {
                    _FileInfoDal = AbstractFactory.CreateFileInfoDAL();
                }
                return _FileInfoDal;
            }

            set { _FileInfoDal = value; }
        }
		        private IDAL.IOrderInfoDAL _OrderInfoDal;

        public IOrderInfoDAL OrderInfoDAL
        {
            get
            {
                if (_OrderInfoDal == null)
                {
                    _OrderInfoDal = AbstractFactory.CreateOrderInfoDAL();
                }
                return _OrderInfoDal;
            }

            set { _OrderInfoDal = value; }
        }
		        private IDAL.IR_UserInfo_ActionInfoDAL _R_UserInfo_ActionInfoDal;

        public IR_UserInfo_ActionInfoDAL R_UserInfo_ActionInfoDAL
        {
            get
            {
                if (_R_UserInfo_ActionInfoDal == null)
                {
                    _R_UserInfo_ActionInfoDal = AbstractFactory.CreateR_UserInfo_ActionInfoDAL();
                }
                return _R_UserInfo_ActionInfoDal;
            }

            set { _R_UserInfo_ActionInfoDal = value; }
        }
		        private IDAL.IRoleInfoDAL _RoleInfoDal;

        public IRoleInfoDAL RoleInfoDAL
        {
            get
            {
                if (_RoleInfoDal == null)
                {
                    _RoleInfoDal = AbstractFactory.CreateRoleInfoDAL();
                }
                return _RoleInfoDal;
            }

            set { _RoleInfoDal = value; }
        }
		        private IDAL.IsysdiagramsDAL _sysdiagramsDal;

        public IsysdiagramsDAL sysdiagramsDAL
        {
            get
            {
                if (_sysdiagramsDal == null)
                {
                    _sysdiagramsDal = AbstractFactory.CreatesysdiagramsDAL();
                }
                return _sysdiagramsDal;
            }

            set { _sysdiagramsDal = value; }
        }
		        private IDAL.IUserInfoDAL _UserInfoDal;

        public IUserInfoDAL UserInfoDAL
        {
            get
            {
                if (_UserInfoDal == null)
                {
                    _UserInfoDal = AbstractFactory.CreateUserInfoDAL();
                }
                return _UserInfoDal;
            }

            set { _UserInfoDal = value; }
        }
		        private IDAL.IWF_InstanceDAL _WF_InstanceDal;

        public IWF_InstanceDAL WF_InstanceDAL
        {
            get
            {
                if (_WF_InstanceDal == null)
                {
                    _WF_InstanceDal = AbstractFactory.CreateWF_InstanceDAL();
                }
                return _WF_InstanceDal;
            }

            set { _WF_InstanceDal = value; }
        }
		        private IDAL.IWF_StepInfoDAL _WF_StepInfoDal;

        public IWF_StepInfoDAL WF_StepInfoDAL
        {
            get
            {
                if (_WF_StepInfoDal == null)
                {
                    _WF_StepInfoDal = AbstractFactory.CreateWF_StepInfoDAL();
                }
                return _WF_StepInfoDal;
            }

            set { _WF_StepInfoDal = value; }
        }
		        private IDAL.IWF_TempDAL _WF_TempDal;

        public IWF_TempDAL WF_TempDAL
        {
            get
            {
                if (_WF_TempDal == null)
                {
                    _WF_TempDal = AbstractFactory.CreateWF_TempDAL();
                }
                return _WF_TempDal;
            }

            set { _WF_TempDal = value; }
        }
		    }
}