using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaplikOtomasyonuDP
{
    class UnitOfWorkFactory
    {
        public enum connectiontype
        {
            SQL,
            ORACLE
        }

        public static IUnitOfWork Create(connectiontype type)
        {

            if (type == connectiontype.SQL)
            {
                //Data Source=MACBOOK-PRO\SQLEXPRESS;Initial Catalog=KITAPLIKOTOMASYONU;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
                //Data Source=WISSEN\MSSQL2017;Initial Catalog=KITAPLIKOTOMASYONU;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
                SqlConnection connection = new SqlConnection
             (@"Data Source=WISSEN\MSSQL2017;Initial Catalog=KITAPLIKOTOMASYONU;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true");
                connection.Open();
                return new AdoNetUnitOfWork(connection, true);
            }
            else
            {
                throw new NotImplementedException();
            }

            return null;

        }
    }
}
