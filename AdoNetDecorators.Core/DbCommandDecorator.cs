using System;
using System.Data;
using System.Data.Common;

namespace AdoNetDecorators.Core
{
    public class DbCommandDecorator : DbCommand
    {
        private readonly DbCommand targetCommand;

        public DbCommandDecorator(DbCommand targetCommand)
        {
            if (targetCommand == null)
            {
                throw new ArgumentNullException("targetCommand", "Cannot decorate null command");
            }

            this.targetCommand = targetCommand;
        }

        public DbCommand TargetCommand
        {
            get
            {
                return targetCommand;
            }
        }

        public override void Prepare()
        {
            targetCommand.Prepare();
        }

        public override string CommandText
        {
            get
            {
                return targetCommand.CommandText;
            }
            set
            {
                targetCommand.CommandText = value;
            }
        }

        public override int CommandTimeout
        {
            get
            {
                return targetCommand.CommandTimeout;
            }
            set
            {
                targetCommand.CommandTimeout = value;
            }
        }

        public override CommandType CommandType
        {
            get
            {
                return targetCommand.CommandType;
            }
            set
            {
                targetCommand.CommandType = value;
            }
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get
            {
                return targetCommand.UpdatedRowSource;
            }
            set
            {
                targetCommand.UpdatedRowSource = value;
            }
        }

        protected override DbConnection DbConnection
        {
            get
            {
                DbConnectionDecorator loggableDbConnection = targetCommand.Connection as DbConnectionDecorator;

                if (loggableDbConnection != null)
                {
                    return loggableDbConnection.TargetConnection;
                }

                return targetCommand.Connection;
            }
            set
            {
                DbConnectionDecorator loggableDbConnection = value as DbConnectionDecorator;

                if (loggableDbConnection != null)
                {
                    targetCommand.Connection = value;
                }
                else
                {
                    targetCommand.Connection = value != null ? new DbConnectionDecorator(value) : null;
                }
            }
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get
            {
                return targetCommand.Parameters;
            }
        }

        protected override DbTransaction DbTransaction
        {
            get
            {
                return targetCommand.Transaction;
            }
            set
            {
                targetCommand.Transaction = value;
            }
        }

        public override bool DesignTimeVisible
        {
            get
            {
                return targetCommand.DesignTimeVisible;
            }
            set
            {
                targetCommand.DesignTimeVisible = value;
            }
        }

        public override void Cancel()
        {
            targetCommand.Cancel();
        }

        protected override DbParameter CreateDbParameter()
        {
            return targetCommand.CreateParameter();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior commandBehavior)
        {
            return targetCommand.ExecuteReader(commandBehavior);
        }

        public override int ExecuteNonQuery()
        {
            return targetCommand.ExecuteNonQuery();
        }

        public override object ExecuteScalar()
        {
            return targetCommand.ExecuteScalar();
        }
    }
}