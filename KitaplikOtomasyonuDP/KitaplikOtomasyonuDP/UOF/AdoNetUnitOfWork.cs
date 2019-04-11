using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaplikOtomasyonuDP
{
    public class AdoNetUnitOfWork : IUnitOfWork,IDisposable
    {
        //public bool _hasConnection
        //{
        //    get;
        //    set;
        //}
        public bool _ownsConnection
        {
            get ;
            set ;
        }
        public IDbTransaction _transaction
        {
            get;
            set;
        }
        public IDbConnection _connection
        {
            get;
            set ;
        }
        public AdoNetUnitOfWork(IDbConnection connection, bool ownsConnection)
        {
            _connection = connection;
            _ownsConnection = ownsConnection;
            _transaction = connection.BeginTransaction();
        }

        public IDbCommand CreateCommand()
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException
                 ("Transaction have already been committed. Check your transaction handling.");

            _transaction.Commit();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (_connection != null && _ownsConnection)
            {
                _connection.Close();
                _connection = null;
            }
        }
        
    }
}
