using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Calendar
{
    class Connection
    {
        string connection;
        public SqlConnection sqlConnection;

        public Connection(string connection)
        {
            this.connection = connection;
            try
            {
                sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();
            }
            catch
            {
                MessageBox.Show(String.Format($"Соединение не обнаруженно, проверьте правильность пути: {connection}"));
                Environment.Exit(0);
            }

        }
    }
}
