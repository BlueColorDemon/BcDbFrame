namespace BcDbFrame.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using System.Data.Entity.ModelConfiguration;
    using BcDbFrame.Entity.ApiBase;

    public partial class ModelAC : DbContext
    {
        public ModelAC()
            : base("name=ModelAC")
        {
        }

        #region ApiBase_

        public DbSet<BaseFunction> BaseFunction { get; set; }
        public DbSet<BaseRole> BaseRole { get; set; }
        public DbSet<BaseUser> BaseUser { get; set; }
        public DbSet<BaseUserDetail> BaseUserDetail { get; set; }
        public DbSet<BaseApp> BaseApp { get; set; }
        public DbSet<BaseApiRecord> BaseApiRecord { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BaseFunctionMap());
            modelBuilder.Configurations.Add(new BaseRoleMap());
            modelBuilder.Configurations.Add(new BaseUserMap());
            modelBuilder.Configurations.Add(new BaseUserDetailMap());
            modelBuilder.Configurations.Add(new BaseAppMap());
            modelBuilder.Configurations.Add(new BaseLogMap());
            modelBuilder.Configurations.Add(new BaseApiRecordMap());

            #region ·ÏÆú
            //string mapSuffix = "BcDbFrame.Entity.ApiBase";

            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //    .Where(type => type.Namespace.EndsWith(mapSuffix, StringComparison.OrdinalIgnoreCase))
            //    .Where(type => !String.IsNullOrEmpty(type.Namespace))
            //    .Where(type => type.BaseType != null && type.BaseType.IsGenericType
            //        && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.Configurations.Add(configurationInstance);
            //} 
            #endregion

            base.OnModelCreating(modelBuilder);
        }




    }
}
