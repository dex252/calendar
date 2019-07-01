using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Calendar
{
    public partial class Calendar : Form
    {
        Connection connection;
        Cash cash;

        public Calendar()
        {
            InitializeComponent();
            Config config = new Config();
            connection = new Connection(config.connect);
            cash = new Cash(connection.sqlConnection, this);

        }

        private void Close_db(object sender, FormClosingEventArgs e)
        {
            if (connection.sqlConnection != null && connection.sqlConnection.State != ConnectionState.Closed)
            {
                connection.sqlConnection.Close();
            }
        }

        private void SelectPopulation(object sender, EventArgs e)
        {
            int currentMyComboBoxIndex = generationsBox.SelectedIndex;
            cash.render.GetPopulation(currentMyComboBoxIndex);
        }
    }
}
