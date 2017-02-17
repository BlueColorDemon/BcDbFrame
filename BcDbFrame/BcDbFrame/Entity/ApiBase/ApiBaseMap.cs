using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcDbFrame.Entity.ApiBase
{

    /// <summary>
    /// 映射信息（Fluent API 配置）
    /// </summary>
    public class BaseFunctionMap : EntityTypeConfiguration<BaseFunction>
    {
        public BaseFunctionMap()
        {
            HasKey(e => new { e.Controller, e.Action });

            Property(e => e.Controller).IsRequired().HasMaxLength(200).IsUnicode();
            Property(e => e.Action).IsRequired().HasMaxLength(200).IsUnicode();
            Property(e => e.ApiCode).IsRequired().HasMaxLength(200).IsUnicode();

            ToTable("BaseFunction");
        }
    }

    /// <summary>
    /// 映射信息（Fluent API 配置）
    /// </summary>
    public class BaseRoleMap : EntityTypeConfiguration<BaseRole>
    {
        public BaseRoleMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Name).IsRequired().HasMaxLength(200).IsUnicode();
            Property(e => e.Category).IsRequired();

            HasOptional(e => e.Parent).WithMany(e => e.Children).HasForeignKey(e => e.ParentID);//从体设置（等同）→主体设置
            HasMany(e => e.Children).WithOptional(e => e.Parent).HasForeignKey(e => e.ParentID);//主体设置（等同）→从体设置

            HasMany(e => e.Users).WithMany(e => e.Roles).Map(m =>
            {
                m.MapLeftKey("Role_Id");
                m.MapRightKey("User_Id");
                m.ToTable("BaseRoleUser");
            });
            HasMany(e => e.Functions).WithMany(e => e.Roles).Map(m =>
            {
                m.MapLeftKey("Role_Id");
                m.MapRightKey("Controller", "Action");//new { e.Controller, e.Action }
                m.ToTable("BaseRoleFunction");
            });
            HasMany(e => e.Apps).WithMany(e => e.Roles).Map(m =>
            {
                m.MapLeftKey("Role_Id");
                m.MapRightKey("App_Id");
                m.ToTable("BaseRoleApp");
            });

            ToTable("BaseRole");
        }
    }

    /// <summary>
    /// 映射信息（Fluent API 配置）
    /// </summary>
    public class BaseUserMap : EntityTypeConfiguration<BaseUser>
    {
        public BaseUserMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Account).IsRequired().HasMaxLength(200).IsUnicode();
            Property(e => e.Password).IsRequired().HasMaxLength(200).IsUnicode();

            //HasRequired(e => e.UserDetail).WithRequiredPrincipal(e => e.User).WillCascadeOnDelete(true);
            HasOptional(e => e.UserDetail).WithRequired(e => e.User).WillCascadeOnDelete(true);//主体设置（等同）→从体设置
            HasMany(e => e.Apps).WithRequired(e => e.User).HasForeignKey(e => e.User_ID);

            ToTable("BaseUser");
        }
    }

    /// <summary>
    /// 映射信息（Fluent API 配置）
    /// </summary>
    public class BaseUserDetailMap : EntityTypeConfiguration<BaseUserDetail>
    {
        public BaseUserDetailMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Name).HasMaxLength(200).IsUnicode();
            Property(e => e.Email).IsRequired().HasMaxLength(200).IsUnicode();
            Property(e => e.Phone).HasMaxLength(200).IsUnicode();
            Property(e => e.Birthday);
            Property(e => e.Height);
            Property(e => e.Note).IsMaxLength().IsUnicode();

            HasRequired(e => e.User).WithOptional(e => e.UserDetail).WillCascadeOnDelete(true);//从体设置（等同）→主体设置

            ToTable("BaseUserDetail");
        }
    }

    /// <summary>
    /// 映射信息（Fluent API 配置）
    /// </summary>
    public class BaseAppMap : EntityTypeConfiguration<BaseApp>
    {
        public BaseAppMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Secret).IsRequired().HasMaxLength(200).IsUnicode();
            Property(e => e.Name).IsRequired().HasMaxLength(200).IsUnicode();
            Property(e => e.Description).IsMaxLength().IsUnicode();

            ToTable("BaseApp");
        }
    }

    /// <summary>
    /// 映射信息（Fluent API 配置）
    /// </summary>
    public class BaseLogMap : EntityTypeConfiguration<BaseLog>
    {
        public BaseLogMap()
        {
            HasKey(e => e.Id);

            Property(e => e.Message).IsRequired().IsMaxLength().IsUnicode();

            ToTable("BaseLog");
        }
    }

    /// <summary>
    /// 映射信息（Fluent API 配置）
    /// </summary>
    public class BaseApiRecordMap : EntityTypeConfiguration<BaseApiRecord>
    {
        public BaseApiRecordMap()
        {
            HasKey(e => e.Id);

            Property(e => e.ApiCode).HasMaxLength(200).IsUnicode();
            Property(e => e.Visitor);
            Property(e => e.VisitDetail).IsRequired().IsMaxLength().IsUnicode();
            Property(e => e.Input).IsMaxLength().IsUnicode();
            Property(e => e.Output).IsMaxLength().IsUnicode();
            Property(e => e.TimeLong);

            ToTable("BaseApiRecord");
        }
    }


}
