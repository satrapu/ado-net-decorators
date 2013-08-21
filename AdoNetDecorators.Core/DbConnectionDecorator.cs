using System;
using System.Data;
using System.Data.Common;

namespace AdoNetDecorators.Core
{
    public class DbConnectionDecorator : DbConnection
    {
        private readonly DbConnection targetConnection;

        public DbConnectionDecorator(DbConnection targetConnection)
        {
            if (targetConnection == null)
            {
                throw new ArgumentNullException("targetConnection", "Cannot decorate null connection");
            }

            this.targetConnection = targetConnection;
        }

        public DbConnection TargetConnection
        {
            get
            {
                return targetConnection;
            }
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return targetConnection.BeginTransaction(isolationLevel);
        }

        public override void Close()
        {
            targetConnection.Close();
        }

        public override void ChangeDatabase(string databaseName)
        {
            targetConnection.ChangeDatabase(databaseName);
        }

        public override void Open()
        {
            targetConnection.Open();
        }

        public override string ConnectionString
        {
            get;
            set;
        }

        public override string Database
        {
            get
            {
                return targetConnection.Database;
            }
        }

        public override ConnectionState State
        {
            get
            {
                return targetConnection.State;
            }
        }

        public override string DataSource
        {
            get
            {
                return targetConnection.DataSource;
            }
        }

        public override string ServerVersion
        {
            get
            {
                return targetConnection.ServerVersion;
            }
        }

        protected override DbCommand CreateDbCommand()
        {
            DbCommand dbCommand = targetConnection.CreateCommand();
            DbCommandDecorator loggableDbCommand = new DbCommandDecorator(dbCommand);
            return loggableDbCommand;
        }
    }
}