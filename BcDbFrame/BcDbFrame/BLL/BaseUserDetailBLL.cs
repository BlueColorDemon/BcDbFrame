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
    public partial class BaseUserDetailBLL : BaseBLL<BaseUserDetail>
    {
        private readonly BaseDAL<BaseUserDetail> _BaseUserDetailDal;
        public BaseUserDetailBLL()
            : base()
        {
            _BaseUserDetailDal = base.Dal;
        }

		#region 单例模式


        public static BaseUserDetailBLL Instance
        {
            get
            {
                return Singleton<BaseUserDetailBLL>.Instance;
            }
        }

        #endregion
    }
}
    