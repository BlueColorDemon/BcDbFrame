

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
    public partial class BaseFunctionBLL : BaseBLL<BaseFunction>
    {
        private readonly BaseDAL<BaseFunction> _BaseFunctionDal;
        public BaseFunctionBLL()
            : base()
        {
            _BaseFunctionDal = base.Dal;
        }

		#region 单例模式


        public static BaseFunctionBLL Instance
        {
            get
            {
                return Singleton<BaseFunctionBLL>.Instance;
            }
        }

        #endregion
    }
}
    