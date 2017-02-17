using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BcDbFrame.Common;

namespace BcDbFrame.DAL
{
    public class BaseDAL<T> where T : class
    {
        protected readonly DbContext _DbContext;
        protected readonly DbSet<T> _DbSet;

        public BaseDAL()
            : this(new BcDbFrame.Entity.ModelAC())
        { }

        public BaseDAL(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException(" DbContext is null ! ");
            }
            this._DbContext = context;
            this._DbSet = _DbContext.Set<T>();
        }

        /// <summary>
        /// 获取DbContext
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            return _DbContext;
        }

        /// <summary>
        /// 返回IQueryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetQuery()
        {
            return _DbSet;
        }

        /// <summary>
        /// 根据条件查询获取对象（未执行）
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return _DbSet.Where(where);
        }

        /// <summary>
        /// 根据表达式获取单个对象（已执行）
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual T Single(Expression<Func<T, bool>> where)
        {
            return _DbSet.SingleOrDefault(where);
        }

        /// <summary>
        /// 主键查询性能高于Where（已执行）
        /// </summary>
        /// <param name="keyValues">主键</param>
        /// <returns></returns>
        public virtual T GetEntityByID(params object[] keyValues)
        {
            return _DbSet.Find(keyValues);
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
            try
            {
                _DbSet.Add(entity);
                if (isExecute)
                {
                    return _DbContext.SaveChanges() > 0;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

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
            try
            {
                _DbContext.Entry<T>(entity).State = EntityState.Modified;
                if (isExecute)
                {
                    return _DbContext.SaveChanges() > 0;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }

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
            if (_DbContext.Entry(entity).State == EntityState.Detached)
            {
                _DbSet.Attach(entity);
            }
            _DbSet.Remove(entity);
            if (isExecute)
            {
                return _DbContext.SaveChanges() > 0;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 删除实体
        /// （isExecute=false 将主键查询出的实体标记为“删除”状态添加到集的基础上下文中，这样一来，当调用 Save 时，将从数据库中删除该实体）
        /// </summary>
        /// <param name="isExecute">是否执行{True执行（返回影响行数），Flase未执行（返回-1）}</param>
        /// <param name="keyValues">主键</param>
        /// <returns></returns>
        public virtual bool Delete(bool isExecute = true, params object[] keyValues)
        {
            if (keyValues == null && keyValues.Count() < 1)
            {
                return false;
            }
            T entity = _DbSet.Find(keyValues);
            if (_DbContext.Entry(entity).State == EntityState.Detached)
            {
                _DbSet.Attach(entity);
            }
            _DbSet.Remove(entity);
            if (isExecute)
            {
                return _DbContext.SaveChanges() > 0;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 将在此上下文中所做的所有更改保存到基础数据库
        /// </summary>
        /// <returns>已写入基础数据库的对象的数目（返回影响行数）</returns>
        public virtual int Save()
        {
            return _DbContext.SaveChanges();
        }

        /// <summary>
        /// 构造查询条件（未执行），返回IQueryable
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <param name="orderBy">排序列</param>
        /// <param name="desc">升（降）序</param>
        /// <param name="includeProperties">相关对象</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetQuery(Expression<Func<T, bool>> where, string orderBy, bool desc, string includeProperties)
        {
            IQueryable<T> query = this._DbSet;
            if (where != null)
            {
                query = query.Where(where);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return Common.QueryableExtensions.OrderBy<T>(query, orderBy, desc);
            }
            return query;
        }

        /// <summary>
        /// 构造查询条件（已执行），返回IEnumerable
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = this._DbSet;
            if (where != null)
            {
                query = query.Where(where);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// 数据查询后分页
        /// </summary>
        /// <param name="total">总记录数</param>
        /// <param name="query">过滤条件</param>
        /// <param name="page">当前页码</param>
        /// <param name="rows">每页行数</param>
        /// <param name="orderBy">排序列</param>
        /// <param name="desc">升（降）序</param>
        /// <returns></returns>
        public IQueryable<T> GetPager(out int total, Expression<Func<T, Boolean>> where = null, int page = 1, int rows = 20, string orderBy = "", bool desc = false)
        {
            IQueryable<T> query = this.GetQuery(where, orderBy, desc, "");
            total = query.Count();
            query = query.OrderBy(orderBy, desc);
            query = query.Skip((page - 1) * rows).Take(rows);
            return query;
        }
    }
}
