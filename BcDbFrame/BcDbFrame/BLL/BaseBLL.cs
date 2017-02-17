using BcDbFrame.Common;
using BcDbFrame.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BcDbFrame.BLL
{
    public class BaseBLL<T> where T : class
    {
        private BaseDAL<T> _Dal;
        public BaseBLL()
        {
            _Dal = new BaseDAL<T>();
        }

        protected BaseDAL<T> Dal
        {
            get { return _Dal; }
        }

        /// <summary>
        /// 返回IQueryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetQuery()
        {
            return _Dal.GetQuery();
        }

        /// <summary>
        /// 根据条件查询获取对象（未执行）
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return _Dal.Where(where);
        }

        /// <summary>
        /// 根据表达式获取单个对象（已执行）
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual T Single(Expression<Func<T, bool>> where)
        {
            return _Dal.Single(where);
        }

        /// <summary>
        /// 主键查询性能高于Where（已执行）
        /// </summary>
        /// <param name="keyValues">主键</param>
        /// <returns></returns>
        public virtual T GetEntityByID(params object[] keyValues)
        {
            return _Dal.GetEntityByID(keyValues);
        }

        /// <summary>
        /// 添加实体
        /// （isExecute=false 将给定实体以“已添加”状态添加到集的基础上下文中，这样一来，当调用 Save 时，会将该实体插入到数据库中）
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <param name="isExecute">是否执行{True执行（返回影响行数），Flase未执行（返回-1）}</param>
        /// <returns></returns>
        public virtual bool Add(T entity, bool isExecute = true)
        {
            return _Dal.Add(entity, isExecute);
        }

        /// <summary>
        /// 更新实体
        /// （isExecute=false 将给定实体标记为“更新”状态添加到集的基础上下文中，这样一来，当调用 Save 时，会将该实体更新到数据库中）
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <param name="isExecute">是否执行{True执行（返回影响行数），Flase未执行（返回-1）}</param>
        /// <returns></returns>
        public virtual bool Update(T entity, bool isExecute = true)
        {
            return _Dal.Update(entity, isExecute);
        }

        /// <summary>
        /// 删除实体
        /// （isExecute=false 将给定实体标记为“删除”状态添加到集的基础上下文中，这样一来，当调用 Save 时，将从数据库中删除该实体）
        /// </summary>
        /// <param name="entity">要添加的实体</param>
        /// <param name="isExecute">是否执行{True执行（返回影响行数），Flase未执行（返回-1）}</param>
        /// <returns></returns>
        public virtual bool Delete(T entity, bool isExecute = true)
        {
            return _Dal.Delete(entity, isExecute);
        }

        /// <summary>
        /// <summary>
        /// 删除实体
        /// （isExecute=false 将主键查询出的实体标记为“删除”状态添加到集的基础上下文中，这样一来，当调用 Save 时，将从数据库中删除该实体）
        /// </summary>
        /// <param name="isExecute">是否执行{True执行（返回影响行数），Flase未执行（返回-1）}</param>
        /// <param name="keyValues">主键</param>
        /// <returns></returns>
        /// </summary>
        /// <param name="isExecute"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual bool Delete(bool isExecute = true, params object[] keyValues)
        {
            return _Dal.Delete(isExecute, keyValues);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="idList">ID字符串（1,2,3）</param>
        /// <param name="dataType">键值类型</param>
        /// <param name="msg">输出消息</param>
        /// <returns></returns>
        public virtual bool Delete(string idList, DataType dataType, out string msg)
        {
            if (idList.IsEmpty())
            {
                msg = SysMessage.ParamError.ToMessage();
                return false;
            }
            string[] ids = idList.Split(',');
            if (dataType == DataType.String)
            {
                foreach (string id in ids)
                {
                    _Dal.Delete(false, id);
                }
            }
            else if (dataType == DataType.Number)
            {
                foreach (string id in ids)
                {
                    _Dal.Delete(false, id.ToInt32());
                }
            }
            bool result = _Dal.Save() > 0;
            msg = SysOperate.Delete.ToMessage(result);
            return result;
        }
    }
}
