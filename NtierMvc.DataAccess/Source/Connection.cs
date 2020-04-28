using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NtierMvc.DataAccess.Source
{
    // Singleton Class
    public class Connection
    {
        // Connection's configuration:
        private static string connectionString = ConfigurationManager.ConnectionStrings["NtierMvc.Connection"].ConnectionString;
        private static Connection singleton;
        private static SqlConnection sqlConnection;

        public SqlConnection SqlConnetionFactory
        {
            get { return sqlConnection; }
        }

        private Connection() { }

        public static Connection Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new Connection();

                sqlConnection = new SqlConnection(connectionString);
                return singleton;
            }
        }

    }

    public class Connection2
    {
        // Connection's configuration:
        private static string connectionString2 = ConfigurationManager.ConnectionStrings["NtierMvc.Connection"].ConnectionString;
        private static Connection2 singleton2;
        private static SqlConnection sqlConnection2;

        public SqlConnection SqlConnetionFactory2
        {
            get { return sqlConnection2; }
        }

        private Connection2() { }

        public static Connection2 Singleton2
        {
            get
            {
                if (singleton2 == null)
                    singleton2 = new Connection2();

                sqlConnection2 = new SqlConnection(connectionString2);
                return singleton2;
            }
        }

    }
}
