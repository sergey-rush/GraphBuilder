using System;
using System.Configuration;
using System.IO;

namespace GraphBuilder.Data
{
    public class Context
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["SqlServer"].ToString();
        public static string ConnectionString
        {
            get
            {
                string database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _connectionString);
                string connectionString = @"Data Source=" + Path.GetFullPath(database);
                //return connectionString;
                return connectionString;
            }
        }

        public static DataManager Data
        {
            get { return DataManager.Instance; }
        }
    }
}