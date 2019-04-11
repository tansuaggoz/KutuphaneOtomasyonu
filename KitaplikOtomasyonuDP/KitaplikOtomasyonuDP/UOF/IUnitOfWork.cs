using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaplikOtomasyonuDP
{
    public interface IUnitOfWork
    {
        //bool _hasConnection { get; set; }//conn var mı

        bool _ownsConnection { get; set; }//bağlantı sahibi var mı
        IDbTransaction _transaction { get; set; }
        IDbConnection _connection { get; set; }
        IDbCommand CreateCommand();
        void SaveChanges();
        void Dispose();
    }
}
