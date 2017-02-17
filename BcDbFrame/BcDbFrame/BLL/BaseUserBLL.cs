using BcDbFrame.DAL;
using BcDbFrame.Entity.ApiBase;
using BcDbFrame.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcDbFrame.BLL.ApiBase
{
    public partial class BaseUserBLL : BaseBLL<BaseUser>
    {
        private readonly BaseDAL<BaseUser> _BaseUserDal;
        public BaseUserBLL()
            : base()
        {
            _BaseUserDal = base.Dal;
        }

		#region 单例模式


        public static BaseUserBLL Instance
        {
            get
            {
                return Singleton<BaseUserBLL>.Instance;
            }
        }

        #endregion
    }
}
    