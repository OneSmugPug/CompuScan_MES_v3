using System.Data.SqlClient;

namespace CompuScan_MES_Main
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            return DBConnection.GetDBConnection("192.168.8.121\\QTSQLSERVER,1433", "QTech_Compuscan", "User01", "12345");

            //return DBConnection.GetDBConnection("192.168.1.254\\MSSQLSERVER,1433", "Compuscan", "sa", "Sasa123");
        }
    }
}
