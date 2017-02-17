using BcDbFrame.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcDbFrame.Entity.ApiBase
{
    #region 基本-功能模块


    /// <summary>
    /// 基本-功能模块
    /// </summary>
    public class BaseFunction : BaseModel
    {
        public BaseFunction()
            : base()
        {
            Category = BaseFunctionCategory.Default;
            Roles = new HashSet<BaseRole>();
        }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public BaseFunctionCategory Category { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string ApiCode { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        public virtual ICollection<BaseRole> Roles { get; set; }
    }


    /// <summary>
    /// 基本-功能模块,功能类别
    /// </summary>
    public enum BaseFunctionCategory
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 供管理使用
        /// </summary>
        Admin = 1,
        /// <summary>
        /// 供客户使用
        /// </summary>
        Customer = 2,
        /// <summary>
        /// 供商业使用
        /// </summary>
        Bunesss = 3,
        /// <summary>
        /// 供测试使用
        /// </summary>
        Test = 4
    }


    #endregion


    #region 用户角色


    /// <summary>
    /// 基本-角色
    /// </summary>
    public class BaseRole : BaseModel
    {
        public BaseRole()
            : base()
        {
            Category = BaseRoleCategory.Default;
            Children = new HashSet<BaseRole>();
            Users = new HashSet<BaseUser>();
            Functions = new HashSet<BaseFunction>();
            Apps = new HashSet<BaseApp>();
        }


        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 父角色编号
        /// </summary>
        public Guid? ParentID { get; set; }


        /// <summary>
        /// 角色分类
        /// </summary>
        public BaseRoleCategory Category { get; set; }


        /// <summary>
        /// 子角色集合
        /// </summary>
        public virtual ICollection<BaseRole> Children { get; set; }


        /// <summary>
        /// 父角色
        /// </summary>
        public virtual BaseRole Parent { get; set; }


        /// <summary>
        /// 用户集合
        /// </summary>
        public virtual ICollection<BaseUser> Users { get; set; }


        /// <summary>
        /// 功能集合
        /// </summary>
        public virtual ICollection<BaseFunction> Functions { get; set; }


        /// <summary>
        /// App集合
        /// </summary>
        public virtual ICollection<BaseApp> Apps { get; set; }
    }


    /// <summary>
    /// 基本-用户
    /// </summary>
    public class BaseUser : BaseModel
    {
        public BaseUser()
            : base()
        {
            Category = BaseUserCategory.Default;
            Roles = new HashSet<BaseRole>();
            Apps = new HashSet<BaseApp>();
        }


        /// <summary>
        /// 账户名
        /// </summary>
        public string Account { get; set; }


        /// <summary>
        /// 账户密码
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 账户分类
        /// </summary>
        public BaseUserCategory Category { get; set; }


        /// <summary>
        /// 账户角色集合
        /// </summary>
        public virtual ICollection<BaseRole> Roles { get; set; }


        /// <summary>
        /// App集合
        /// </summary>
        public virtual ICollection<BaseApp> Apps { get; set; }


        /// <summary>
        /// 对应账户
        /// </summary>
        public virtual BaseUserDetail UserDetail { get; set; }
    }


    /// <summary>
    /// 基本-用户详情
    /// </summary>
    public class BaseUserDetail : BaseModel
    {
        public BaseUserDetail()
            : base()
        {
            Sex = BaseSex.None;
        }


        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        public BaseSex Sex { get; set; }


        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }


        /// <summary>
        /// 身高
        /// </summary>
        public decimal? Height { get; set; }


        /// <summary>
        /// 简介
        /// </summary>
        public string Note { get; set; }


        /// <summary>
        /// 对应账户
        /// </summary>
        public virtual BaseUser User { get; set; }
    }


    /// <summary>
    /// 性别
    /// </summary>
    public enum BaseSex
    {
        /// <summary>
        /// 女
        /// </summary>
        Girl = 0,
        /// <summary>
        /// 男
        /// </summary>
        Boy = 1,
        /// <summary>
        /// 未知
        /// </summary>
        None = 2
    }


    /// <summary>
    /// 基本-角色类别
    /// </summary>
    public enum BaseRoleCategory
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 管理类别
        /// </summary>
        Admin = 1,
        /// <summary>
        /// 客户类别
        /// </summary>
        Customer = 2,
        /// <summary>
        /// App类别
        /// </summary>
        App = 3,
        /// <summary>
        /// 独角色
        /// </summary>
        Single = 4
    }


    /// <summary>
    /// 基本-用户类别
    /// </summary>
    public enum BaseUserCategory
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 管理类别
        /// </summary>
        Admin = 1,
        /// <summary>
        /// 客户类别
        /// </summary>
        Customer = 2
    }


    #endregion


    #region 应用App

    /// <summary>
    /// 基本-应用
    /// </summary>
    public class BaseApp : BaseModel
    {
        public BaseApp()
            : base()
        {
            Category = BaseAppCategory.Default;
            Roles = new HashSet<BaseRole>();
        }

        /// <summary>
        /// App所属用户ID
        /// </summary>
        public Guid User_ID { get; set; }

        /// <summary>
        /// App密钥
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// App名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// App描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// App分类
        /// </summary>
        public BaseAppCategory Category { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        public virtual ICollection<BaseRole> Roles { get; set; }

        /// <summary>
        /// 对应账户
        /// </summary>
        public virtual BaseUser User { get; set; }

    }

    /// <summary>
    /// 基本-应用,功能类别
    /// </summary>
    public enum BaseAppCategory
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 学校
        /// </summary>
        School = 1,
        /// <summary>
        /// 企业
        /// </summary>
        Enterprise = 2
    }

    #endregion


    #region 日志统计


    /// <summary>
    /// 基本-日志
    /// </summary>
    public class BaseLog : BaseModel
    {

        public BaseLog()
            : base()
        {
            Category = BaseLogCategory.Default;
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 日志分类
        /// </summary>
        public BaseLogCategory Category { get; set; }

    }


    /// <summary>
    /// 基本-日志分类
    /// </summary>
    public enum BaseLogCategory
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 调试
        /// </summary>
        Debug = 1,
        /// <summary>
        /// 信息
        /// </summary>
        Info = 2,
        /// <summary>
        /// 跟踪
        /// </summary>
        Trace = 3,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 4,
        /// <summary>
        /// 严重错误
        /// </summary>
        Fatal = 5
    }


    /// <summary>
    /// 基本-Api记录
    /// </summary>
    public class BaseApiRecord : BaseModel
    {
        /// <summary>
        /// Api代码
        /// </summary>
        public string ApiCode { get; set; }

        /// <summary>
        /// 拜访者
        /// </summary>
        public string Visitor { get; set; }

        /// <summary>
        /// 拜访者详情
        /// </summary>
        public string VisitDetail { get; set; }

        /// <summary>
        /// 输入
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        /// 输出
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// 耗时
        /// </summary>
        public decimal TimeLong { get; set; }

    }


    #endregion


}
