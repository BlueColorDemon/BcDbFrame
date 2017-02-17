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
    public partial class BaseLogBLL : BaseBLL<BaseLog>
    {
        private readonly BaseDAL<BaseLog> _BaseLogDal;
        public BaseLogBLL()
            : base()
        {
            _BaseLogDal = base.Dal;
        }

		#region 单例模式


        public static BaseLogBLL Instance
        {
            get
            {
                return Singleton<BaseLogBLL>.Instance;
            }
        }

        #endregion
    }
}
    