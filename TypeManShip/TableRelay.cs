using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TypeManShip
{
    class TableRelay
    {
        public MySqlDataAdapter adapter;
        public MySqlCommandBuilder builder;

        public TableRelay(MySqlConnection connection, string tablename)
        {
            adapter = new MySqlDataAdapter();
            adapter.TableMappings.Add("Table", tablename);
            MySqlCommand command = new MySqlCommand(string.Format("Select * From {0}", tablename), connection);
            adapter.SelectCommand = command;
            builder = new MySqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
        }

        //public void Fill(ref DataSet data)
        //{
        //    adapter.Fill(data);
        //}
    }
}
