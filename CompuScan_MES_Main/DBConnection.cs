using System.Data.SqlClient;

namespace CompuScan_MES_Main
{
    class DBConnection
    {
        public static SqlConnection GetDBConnection(string datasource, string database, string username, string password)
        {
            return new SqlConnection("Data Source=" + datasource + ";Initial Catalog=" + database + ";Persist Security Info=False;User ID=" + username + ";Password=" + password);
        }
    }
}
