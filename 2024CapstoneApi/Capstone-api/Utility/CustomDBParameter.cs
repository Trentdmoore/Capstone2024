using System.Data;
using System.Data.Common;

namespace Capstone_api.Utility
{
    public class CustomDBParameter : DbCommand
    {
        private readonly DbCommand _command;
        public CustomDBParameter(string name, Object value)
        {
            CreateDbParameter(name, value);
        }
        public CustomDBParameter(DbCommand command)
        {
            _command = command;
        }

        public override string CommandText { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int CommandTimeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CommandType CommandType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override bool DesignTimeVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override UpdateRowSource UpdatedRowSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        protected override DbConnection? DbConnection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override DbParameterCollection DbParameterCollection => throw new NotImplementedException();

        protected override DbTransaction? DbTransaction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public override object? ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        public DbParameter CreateDbParameter(string name, object value)
        {
            try
            {
                var param = _command.CreateParameter();
                param.ParameterName = name;
                param.Value = value;

                return param;
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }
    }
}
