using System;
using System.Data.Common;

namespace AdoNetDecorators.Core
{
    public class DbProviderFactoryDecorator : DbProviderFactory
    {
        private readonly DbProviderFactory targetDbProviderFactory;

        public DbProviderFactoryDecorator(DbProviderFactory targetDbProviderFactory)
        {
            if (targetDbProviderFactory == null)
            {
                throw new ArgumentNullException("targetDbProviderFactory", "Cannot decorate null factory");
            }

            this.targetDbProviderFactory = targetDbProviderFactory;
        }

        public DbProviderFactory TargetDbProviderFactory
        {
            get
            {
                return targetDbProviderFactory;
            }
        }

        public override DbCommand CreateCommand()
        {
            DbCommand dbCommand = base.CreateCommand();
            DbCommandDecorator loggableDbCommand = new DbCommandDecorator(dbCommand);
            return loggableDbCommand;
        }

        public override DbConnection CreateConnection()
        {
            DbConnection dbConnection = base.CreateConnection();
            DbConnectionDecorator loggableDbConnection = new DbConnectionDecorator(dbConnection);
            return loggableDbConnection;
        }
    }
}