using System.Data.SqlClient;

namespace CompuScan_MES_Client
{
    class DBUtils
    {
        public static SqlConnection GetFEMDBConnection()
        {
            return DBConnection.GetDBConnection("192.168.8.121\\QTSQLSERVER,1433", "Valeo_FEM_Line", "User01", "12345");

            //return DBConnection.GetDBConnection("192.168.1.254\\MSSQLSERVER,1433", "Valeo_FEM_Line", "sa", "Sasa123");
        }

        public static SqlConnection GetMainDBConnection()
        {
            return DBConnection.GetDBConnection("192.168.8.121\\QTSQLSERVER,1433", "Valeo_Main", "User01", "12345");

            //return DBConnection.GetDBConnection("192.168.1.254\\MSSQLSERVER,1433", "Valeo_Main", "sa", "Sasa123");
        }
    }
}
