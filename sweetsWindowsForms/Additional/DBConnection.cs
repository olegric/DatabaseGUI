using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace sweetsWindowsForms.Additional
{
    class DBConnection
    {
        //оголошення змінної для зєднання з БД
        private SqlConnection connect = new SqlConnection(); // оголошення змінної для з'єднання з БД 

        public DBConnection()
        {   //рядок для з'єднання з БД
            connect.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\WindowsFormsApp\sweetsWindowsForms\sweetsWindowsForms\Fabrik.mdf;Integrated Security=True;Connect Timeout=30";
        }
        //методи класу
        public void Open()
        {
            connect.Open();
        }
        public void Close()
        {
            connect.Close();
        }
        public SqlCommand Command(String query)
        {
            SqlCommand command = new SqlCommand(query, connect);

            return command;
        }
        public bool RunCommand(String query)
        {
            SqlCommand command = new SqlCommand(query, connect);

            return command.ExecuteNonQuery() > 0;
        }
        public SqlDataReader GetReader(String query)
        {
            SqlCommand command = new SqlCommand(query, connect);

            return command.ExecuteReader();
        }
    }
}
