using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitaplikOtomasyonuDP.Emanetler
{
    public class EmanetlerRepository
    {
        private AdoNetUnitOfWork _unitOfWork;

        public EmanetlerRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("uow");

            _unitOfWork = uow as AdoNetUnitOfWork;
            if (_unitOfWork == null)
                throw new NotSupportedException("Ohh my, change that UnitOfWorkFactory, will you?");
        }
        public List<Emanet> GetAll()
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Emanet";

                SqlDataReader rd = (SqlDataReader)cmd.ExecuteReader();
                List<Emanet> emanetler = new List<Emanet>();
                Emanet emanet = null;
                while (rd.Read())
                {
                    emanet = new Emanet
                    {
                        EID = rd.GetInt32(0),
                        UyeID = rd.GetInt32(1),
                        KitapID = rd.GetInt32(2),
                        VermeTarihi = rd.GetDateTime(3),
                        GeriAlmaTarihi = rd.IsDBNull(5) ?(DateTime?)null : rd.GetDateTime(4),
                        TeslimNot =rd.IsDBNull(5)? null: rd.GetString(5),
                        TeslimEdildiMi =rd.IsDBNull(6)?null: rd.GetString(6),
                        //rd.IsDBNull(5)? null: rd.GetDateTime(4)

                    };
                    emanetler.Add(emanet);
                }
                rd.Close();

                return emanetler;
            }
        }
        public void EmanetEkle(int UID,int KID,string vermeTarihi)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Emanet (UyeID,KitapID,VermeTarihi) VALUES("+UID+","+KID+",CAST('"+vermeTarihi+"' AS DATETIME))";

                cmd.ExecuteNonQuery();
            }
        }
        public void EmanetGeriAl(string geriAlmaTarihi,string teslimNot,string teslimEdildimi,int ID)
        {
            using (var cmd = _unitOfWork.CreateCommand())
            {
                cmd.CommandText = "UPDATE Emanet SET GeriAlmaTarihi=" + "CAST('" + geriAlmaTarihi + "' AS DATETIME)" + ",TeslimNot='" + teslimNot + "',TeslimEdildiMi='" + teslimEdildimi + "' WHERE EID=" + ID;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
