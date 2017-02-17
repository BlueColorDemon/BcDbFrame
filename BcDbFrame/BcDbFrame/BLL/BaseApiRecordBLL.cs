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
    public partial class BaseApiRecordBLL : BaseBLL<BaseApiRecord>
    {
        private readonly BaseDAL<BaseApiRecord> _BaseApiRecordDal;
        public BaseApiRecordBLL()
            : base()
        {
            _BaseApiRecordDal = base.Dal;
        }

		#region 单例模式


        public static BaseApiRecordBLL Instance
        {
            get
            {
                return Singleton<BaseApiRecordBLL>.Instance;
            }
        }

        #endregion
    }
}
    